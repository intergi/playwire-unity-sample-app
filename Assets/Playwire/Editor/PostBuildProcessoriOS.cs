#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

internal class PostBuildProcessoriOS: PostBuildProcessorBase
{
    // Info.plist values (update these with your own values)
    private static string gamAppId = "ca-app-pub-6531503260671471~4637188748";
    private static bool userTrackingEnabled = true;
    private static string userTrackingDescription = "This identifier will be used to deliver personalized ads to you.";

    [PostProcessBuildAttribute(0)]
    public static void OnPostrocessBuild(BuildTarget target, string pathToBuiltProject) 
    {
        if (target == BuildTarget.iOS)
        {
            // Get project into C#
            var projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);

            // Define path with iOS resources
            string iOSResourcesPath = "Assets/Playwire/Plugins/Resources/iOS";

            var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            // Update Info.plist to include necessary parameters
            plist.root.SetString("GADApplicationIdentifier", gamAppId);
            plist.root.SetBoolean("GADIsAdManagerApp", true);
            if (userTrackingEnabled)
            {
                plist.root.SetString("NSUserTrackingUsageDescription", userTrackingDescription);
            }

            // Update Info.plist to include list of required SKAdNetworkIds
            string skAdNetworkIdsPath = Path.Combine(iOSResourcesPath, "skadnetworkids.plist");
            AddSKAdNetworkIds(plist, skAdNetworkIdsPath);

            plist.WriteToFile(plistPath);

            // Get targetGuids
            var unityFrameworkTargetGuid = project.GetUnityFrameworkTargetGuid();
            var unityMainTargetGuid = project.GetUnityMainTargetGuid();

            AddSwiftSupport(pathToBuiltProject, project, unityFrameworkTargetGuid, unityMainTargetGuid);

            // Update Paths of the whole Xcode project
            project.SetBuildProperty(unityMainTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            project.AddBuildProperty(unityMainTargetGuid, "LIBRARY_SEARCH_PATHS", "$(SDKROOT)/usr/lib/swift");
            project.AddBuildProperty(unityMainTargetGuid, "OTHER_LDFLAGS", "-ObjC");
            project.AddBuildProperty(unityMainTargetGuid, "SWIFT_INCLUDE_PATHS", "$(PODS_ROOT)/Playwire/** $(PODS_XCFRAMEWORKS_BUILD_DIR)/**");
            project.AddBuildProperty(unityFrameworkTargetGuid, "EXCLUDED_SOURCE_FILE_NAMES", "$(SRCROOT)/Libraries/Playwire/Plugins/iOS/Tests/*");

            // Save all changes
            project.WriteToFile(projectPath);
        }
    }

    #region iOS

    private static void AddSwiftSupport(string path, PBXProject project, string unityFrameworkTargetGuid, string unityMainTargetGuid)
    {
        var swiftSupportFilePath = "Classes/PlaywireSwiftSupport.swift";
        var swiftSupportTotalFilePath = Path.Combine(path, swiftSupportFilePath);
        if (File.Exists(swiftSupportTotalFilePath))
        {
            var fileToDeleteGuid = project.FindFileGuidByRealPath(swiftSupportTotalFilePath, PBXSourceTree.Source);
            if (!string.IsNullOrEmpty(fileToDeleteGuid))
            {
                project.RemoveFile(fileToDeleteGuid);
                project.RemoveFileFromBuild(unityFrameworkTargetGuid, fileToDeleteGuid);
                project.RemoveFileFromBuild(unityMainTargetGuid, fileToDeleteGuid);
                FileUtil.DeleteFileOrDirectory(swiftSupportTotalFilePath);
            }
        }
        CreateSwiftFile(swiftSupportTotalFilePath);
        var fileGuid = project.AddFile(swiftSupportTotalFilePath, swiftSupportFilePath, PBXSourceTree.Source);

        project.AddFileToBuild(unityFrameworkTargetGuid, fileGuid);
        project.AddBuildProperty(unityFrameworkTargetGuid, "SWIFT_VERSION", "5");
        project.AddBuildProperty(unityFrameworkTargetGuid, "CLANG_ENABLE_MODULES", "YES");

        project.AddFileToBuild(unityMainTargetGuid, fileGuid);
        project.AddBuildProperty(unityMainTargetGuid, "SWIFT_VERSION", "5");
        project.AddBuildProperty(unityMainTargetGuid, "CLANG_ENABLE_MODULES", "YES");
    }

    private static void CreateSwiftFile(string path)
    {
        if (File.Exists(path))
            return;
        var swiftFileAssembler = File.CreateText(path);

        using (swiftFileAssembler)
        {
            swiftFileAssembler.WriteLine("//");
            swiftFileAssembler.WriteLine("//  PlaywireSwiftSupport.swift");
            swiftFileAssembler.WriteLine("//  Playwire");
            swiftFileAssembler.WriteLine("//");
            swiftFileAssembler.WriteLine("//  Created by Intergi. All rights reserved.");
            swiftFileAssembler.WriteLine("//\n");
            swiftFileAssembler.WriteLine("import Foundation");
            swiftFileAssembler.Close();
        }
    }

    private static HashSet<string> ReadSKAdNetworkIds(string path) 
    {
        // Get "SKAdNetworkItems" array from the Resources directory
        var plist = new PlistDocument();
        plist.ReadFromFile(path);

        PlistElement skAdNetworkItems;
        plist.root.values.TryGetValue("SKAdNetworkItems", out skAdNetworkItems);
        var skAdNetworkIds = new HashSet<string>();
 
        if (skAdNetworkItems != null && skAdNetworkItems.GetType() == typeof(PlistElementArray))
        {
            var plistElementDictionaries = skAdNetworkItems.AsArray().values
                                                            .Where(plistElement => plistElement.GetType() == typeof(PlistElementDict));
            // Collect unique "SKAdNetworkIdentifier" from the array                                      
            foreach (var plistElement in plistElementDictionaries)
            {
                PlistElement skAdNetworkId;
                plistElement.AsDict().values.TryGetValue("SKAdNetworkIdentifier", out skAdNetworkId);
                if (skAdNetworkId == null || skAdNetworkId.GetType() != typeof(PlistElementString) || string.IsNullOrEmpty(skAdNetworkId.AsString())) continue;
                skAdNetworkIds.Add(skAdNetworkId.AsString());
            }
        }
        return skAdNetworkIds;
    }

    private static void AddSKAdNetworkIds(PlistDocument plist, string skAdNetworkIdsPlistPath)
    {
        // Read identifiers from the plist in the Resources directory
        var skAdNetworkIds = ReadSKAdNetworkIds(skAdNetworkIdsPlistPath);
        if (skAdNetworkIds == null || skAdNetworkIds.Count < 1)
        {
            return;
        }

        // Get current identifiers from the Info.plist
        PlistElement skAdNetworkItems;
        plist.root.values.TryGetValue("SKAdNetworkItems", out skAdNetworkItems);

        // Check if "SKAdNetworkItems" array is already in the Info.plist and collect "SKAdNetworkIdentifier" that are already present.
        var existingSkAdNetworkIds = new HashSet<string>();
        if (skAdNetworkItems != null && skAdNetworkItems.GetType() == typeof(PlistElementArray))
        {
            var plistElementDictionaries = skAdNetworkItems.AsArray().values.Where(plistElement => plistElement.GetType() == typeof(PlistElementDict));
            foreach (var plistElement in plistElementDictionaries)
            {
                PlistElement existingId;
                plistElement.AsDict().values.TryGetValue("SKAdNetworkIdentifier", out existingId);
                if (existingId == null || existingId.GetType() != typeof(PlistElementString) || string.IsNullOrEmpty(existingId.AsString())) continue;
                existingSkAdNetworkIds.Add(existingId.AsString());
            }
        }
        // The Info.plist doesn't contain "SKAdNetworkItems" array, create a new one
        else
        {
            skAdNetworkItems = plist.root.CreateArray("SKAdNetworkItems");
        }

        foreach (var skAdNetworkId in skAdNetworkIds)
        {
            // Skip existing "SKAdNetworkIdentifier"
            if (existingSkAdNetworkIds.Contains(skAdNetworkId)) continue;
            var skAdNetworkItemDict = skAdNetworkItems.AsArray().AddDict();
            skAdNetworkItemDict.SetString("SKAdNetworkIdentifier", skAdNetworkId);
        }
    }

    #endregion
}
#endif