using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Android;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

internal class PostBuildProcessorAndroid: PostBuildProcessorBase, IPostGenerateGradleAndroidProject
{
    private static string androidAppIdentifierKey = "com.google.android.gms.ads.APPLICATION_ID";
    private static string gamAppId = "YOUR_GAM_APP_ID";

    public int callbackOrder 
    { 
        get 
        { 
            return 0;
        } 
    }

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        string root = System.IO.Directory.GetParent(path).FullName;

        var androidResourcesPath = "Assets/Playwire/Plugins/Resources/Android";
        List<string> resources = new List<string>();

        // Read GAD identifier from the config file
        if(String.IsNullOrEmpty(gamAppId)) 
        {
            Debug.LogWarning($"[{LogTag}] Couldn't find gamAppId in the PostBuildProcessorAndroid.cs.");
            gamAppId = "";
        }

        // Update AndroidManifest.xml to include list of required settings
        SetAndroidPermissions(root, androidResourcesPath);
        SetAndroidFeatures(root, androidResourcesPath);
        SetAndroidMetadata(root, androidResourcesPath, gamAppId);
        SetAndroidActivity(root, androidResourcesPath);
        SetAndroidProperty(root);

        // Update gradle.properties file
        SetAndroidGradleProperties(root);
    }

    # region Android

    private static void SetAndroidPermissions(string path, string resourcesPath)
    {
        string settingsPath = Path.Combine(resourcesPath, "settings.xml");
        XDocument settingsDocument;
        try
        {
            settingsDocument = XDocument.Load(settingsPath);
        }
        catch 
        {
            Debug.LogWarning($"[{LogTag}] settings.xml is not found at {settingsPath}");
            return;
        } 

        string[] permissions = settingsDocument.Element("settings")
                                            .Elements("permission")
                                            .Select(permission => permission.Attribute("name").Value)
                                            .Where(permission=>!String.IsNullOrEmpty(permission))
                                            .ToArray();

        string manifestPath = Path.Combine(path, "launcher/src/main/AndroidManifest.xml");
        XDocument manifest;
        try
        {
            manifest = XDocument.Load(manifestPath);
        } 
        catch 
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is not found at {manifestPath}.");
            return;
        }

        // Get the <manifest> element
        var elementManifest = manifest.Element("manifest");
        if (elementManifest == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <manifest> is not found.");
            return;
        }

        XNamespace androidNamespace = elementManifest.GetNamespaceOfPrefix("android");
        if (androidNamespace == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, \"android\" namespace is not found.");
            return;
        } 

        var descendants = elementManifest.Descendants("uses-permission");
        foreach (var permission in permissions)
        {
            // Looking for existing <uses-permission> elements
            var permissionElement = descendants.FirstOrDefault(descendant => 
                descendant.FirstAttribute != null &&
                descendant.FirstAttribute.Name.LocalName.Equals("name") &&
                descendant.FirstAttribute.Value.Equals(permission)
            );

            // Create a new <uses-permission> element cause it doesn't exist in the AndroidManifest.xml
            if (permissionElement == null)
            {
                var newPermissionElement = new XElement("uses-permission");
                newPermissionElement.Add(new XAttribute(androidNamespace + "name", permission));
                elementManifest.Add(newPermissionElement);
            }
        }
        manifest.Save(manifestPath);
    }

    private static void SetAndroidFeatures(string path, string resourcesPath)
    {
        string settingsPath = Path.Combine(resourcesPath, "settings.xml");
        XDocument settingsDocument;
        try
        {
            settingsDocument = XDocument.Load(settingsPath);
        }
        catch 
        {
            Debug.LogWarning($"[{LogTag}] settings.xml is not found at {settingsPath}");
            return;
        } 

        string manifestPath = Path.Combine(path, "launcher/src/main/AndroidManifest.xml");
        XDocument manifest;
        try
        {
            manifest = XDocument.Load(manifestPath);
        } 
        catch 
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is not found at {manifestPath}.");
            return;
        }

        // Get the <manifest> element
        var elementManifest = manifest.Element("manifest");
        if (elementManifest == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <manifest> is not found.");
            return;
        }

        XNamespace androidNamespace = elementManifest.GetNamespaceOfPrefix("android");
        if (androidNamespace == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, \"android\" namespace is not found.");
            return;
        }

        string[] features = settingsDocument.Element("settings")
                                            .Elements("uses-feature")
                                            .Select(feature => feature.Attribute("name").Value)
                                            .Where(feature => !String.IsNullOrEmpty(feature))
                                            .ToArray();

        var descendants = elementManifest.Descendants("uses-feature");
        foreach (var feature in features)
        {
            // Looking for existing <uses-feature> elements
            var featuresElement = descendants.FirstOrDefault(descendant => 
                descendant.FirstAttribute != null &&
                descendant.FirstAttribute.Name.LocalName.Equals("name") &&
                descendant.FirstAttribute.Value.Equals(feature)
            );

            // Create a new <uses-feature> element cause it doesn't exist in the AndroidManifest.xml
            if (featuresElement == null)
            {
                var newFeatureElement = new XElement("uses-feature");
                newFeatureElement.Add(new XAttribute(androidNamespace + "name", feature));
                elementManifest.Add(newFeatureElement);
            }
        }
        manifest.Save(manifestPath);
    }


    private static void SetAndroidMetadata(string path, string resourcesPath, string gamAppId)
    {

        string settingsPath = Path.Combine(resourcesPath, "settings.xml");
        XDocument settingsDocument;
        try
        {
            settingsDocument = XDocument.Load(settingsPath);
        }
        catch 
        {
            Debug.LogWarning($"[{LogTag}] settings.xml is not found at {settingsPath}");
            return;
        } 

        var values = settingsDocument.Element("settings")
                                   .Elements("metadata")
                                   .Select(metadata => new KeyValuePair<String, String>(metadata.Attribute("name").Value, metadata.Attribute("value").Value))
                                   .Where(metadata=>!String.IsNullOrEmpty(metadata.Key) && (metadata.Value != null))
                                   .ToArray();

        string manifestPath = Path.Combine(path, "launcher/src/main/AndroidManifest.xml");
        XDocument manifest;
        try
        {
            manifest = XDocument.Load(manifestPath);
        } 
        catch 
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is not found at {manifestPath}.");
            return;
        }

        // Get the <manifest> element
        var elementManifest = manifest.Element("manifest");
        if (elementManifest == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <manifest> is not found.");
            return;
        }

        XNamespace androidNamespace = elementManifest.GetNamespaceOfPrefix("android");
        if (androidNamespace == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, \"android\" namespace is not found.");
            return;
        }

        // Get the <application> element
        var elementApplication = elementManifest.Element("application");
        if (elementApplication == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <application> is not found.");
            return;
        }

        var descendants = elementApplication.Descendants("meta-data");
        foreach(var metadata in values)
        {
            // Looking for existing <meta-data> elements
            var elementMetadata = descendants.FirstOrDefault(descendant => 
                descendant.FirstAttribute != null &&
                descendant.FirstAttribute.Name.LocalName.Equals("name") &&
                descendant.FirstAttribute.Value.Equals(metadata.Key) &&
                descendant.LastAttribute != null &&
                descendant.LastAttribute.Name.LocalName.Equals("value")
            );

            // Replace GAD placeholder from the settings.xml with value from the config file
            string value = metadata.Value;
            if (metadata.Key == androidAppIdentifierKey)
            {
                value = gamAppId;
            }

            // Update the <meta-data> element if it exists in the AndroidManifest.xml
            if (elementMetadata != null)
            {
                elementMetadata.LastAttribute.Value = value;
            }
            // Create a new <meta-data> element cause it doesn't exist in the AndroidManifest.xml
            else
            {
                var newElementMetadata = new XElement("meta-data");
                newElementMetadata.Add(new XAttribute(androidNamespace + "name", metadata.Key));
                newElementMetadata.Add(new XAttribute(androidNamespace + "value", value));
                elementApplication.Add(newElementMetadata);
            }
        }

        manifest.Save(manifestPath);
    }

    private static void SetAndroidActivity(string path, string resourcesPath)
    {

        string settingsPath = Path.Combine(resourcesPath, "settings.xml");
        XDocument settingsDocument;
        try
        {
            settingsDocument = XDocument.Load(settingsPath);
        }
        catch 
        {
            Debug.LogWarning($"[{LogTag}] settings.xml is not found at {settingsPath}");
            return;
        } 

        var values = settingsDocument.Element("settings")
                                   .Elements("activity")
                                   .Select(activity => new KeyValuePair<String, String>(activity.Attribute("name").Value, activity.Attribute("value")?.Value ?? null))
                                   .Where(activity=>!String.IsNullOrEmpty(activity.Key))
                                   .ToArray();

        string manifestPath = Path.Combine(path, "launcher/src/main/AndroidManifest.xml");
        XDocument manifest;
        try
        {
            manifest = XDocument.Load(manifestPath);
        } 
        catch 
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is not found at {manifestPath}.");
            return;
        }

        // Get the <manifest> element
        var elementManifest = manifest.Element("manifest");
        if (elementManifest == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <manifest> is not found.");
            return;
        }

        XNamespace androidNamespace = elementManifest.GetNamespaceOfPrefix("android");
        if (androidNamespace == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, \"android\" namespace is not found.");
            return;
        }

        // Get the <application> element
        var elementApplication = elementManifest.Element("application");
        if (elementApplication == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <application> is not found.");
            return;
        }

        var descendants = elementApplication.Descendants("activity");
        foreach(var activity in values)
        {
            // Looking for existing <activity> elements
            var elementActivity = descendants.FirstOrDefault(descendant => 
                descendant.FirstAttribute != null &&
                descendant.FirstAttribute.Name.LocalName.Equals("name") &&
                descendant.FirstAttribute.Value.Equals(activity.Key)
            );

            // Update the <activity> element if it exists in the AndroidManifest.xml
            if (elementActivity != null)
            {
                if (activity.Value != null)
                {
                    elementActivity.LastAttribute.Value = activity.Value;
                }
            }
            // Create a new <activity> element cause it doesn't exist in the AndroidManifest.xml
            else
            {
                var newElementActivity = new XElement("activity");
                newElementActivity.Add(new XAttribute(androidNamespace + "name", activity.Key));
                if (activity.Value != null)
                {
                    newElementActivity.Add(new XAttribute(androidNamespace + "configChanges", activity.Value));
                }
                elementApplication.Add(newElementActivity);
            }
        }
        manifest.Save(manifestPath);
    }

     private static void SetAndroidProperty(string path)
    {
        string manifestPath = Path.Combine(path, "launcher/src/main/AndroidManifest.xml");
        XDocument manifest;
        try
        {
            manifest = XDocument.Load(manifestPath);
        } 
        catch 
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is not found at {manifestPath}.");
            return;
        }

        // Get the <manifest> element
        var elementManifest = manifest.Element("manifest");
        if (elementManifest == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <manifest> is not found.");
            return;
        }

        XNamespace androidNamespace = elementManifest.GetNamespaceOfPrefix("android");
        if (androidNamespace == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, \"android\" namespace is not found.");
            return;
        }

        // Get the <application> element
        var elementApplication = elementManifest.Element("application");
        if (elementApplication == null)
        {
            Debug.LogWarning($"[{LogTag}] AndroidManifest.xml is invalid, <application> is not found.");
            return;
        }

        XNamespace toolsNamespace = @"http://schemas.android.com/tools";
        //var newElementProperty = new XElement("property");
        //newElementProperty.Add(new XAttribute(androidNamespace + "name", "android.adservices.AD_SERVICES_CONFIG"));
        //newElementProperty.Add(new XAttribute(androidNamespace + "resource", "@xml/gma_ad_services_config"));
        //newElementProperty.Add(new XAttribute(toolsNamespace + "replace", "android:resource"));    
        //elementApplication.Add(newElementProperty);
        manifest.Save(manifestPath);
    }

    private static void SetAndroidGradleProperties(string path) { 
        string androidXKey = "android.useAndroidX";
        string enableR8Key = "android.enableR8";

        var gradlePropertiesPath = Path.Combine(path, "gradle.properties");
        var gradleProperties = new List<string>();
        if (File.Exists(gradlePropertiesPath))
        {
            var lines = File.ReadAllLines(gradlePropertiesPath)
                            .Where(line => !line.Contains(enableR8Key))
                            .ToArray();

            #if UNITY_2019_3_OR_NEWER
                var range = lines.Where(line => !line.Contains(androidXKey));
                gradleProperties.AddRange(range);
            #else
                gradleProperties.AddRange(lines);
            #endif
        }

        #if UNITY_2019_3_OR_NEWER
            gradleProperties.Add(androidXKey + "=true");
        #endif

        try
        {
            File.WriteAllText(gradlePropertiesPath, string.Join("\n", gradleProperties.ToArray()) + "\n");
        }
        catch
        {
            Debug.LogWarning($"[{LogTag}] Failed to update file at {gradlePropertiesPath}.");
        }
    }

    # endregion Android
}