using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
internal abstract class PostBuildProcessorBase {

    internal static string LogTag = "PW_BuildPostProcessor";

    # region Helpers
    internal static void CopyAndReplaceFile(string srcPath, string dstPath, string fileName)
    {
        var fullDstPath = Path.Combine(dstPath, Path.GetFileName(fileName));
        var fullSrcPath = Path.Combine(srcPath, Path.GetFileName(fileName));

        if (File.Exists(fullDstPath))
            File.Delete(fullDstPath);
 
        File.Copy(fullSrcPath, fullDstPath);
    }

    internal static void CopyAndReplaceDirectory(string srcPath, string dstPath, string[] enableExts)
    {
        if (Directory.Exists(dstPath))
            Directory.Delete(dstPath, true);
        if (File.Exists(dstPath))
            File.Delete(dstPath);
 
        Directory.CreateDirectory(dstPath);
 
        foreach (var file in Directory.GetFiles(srcPath))
        {
            if (enableExts.Contains(System.IO.Path.GetExtension(file)))
            {
                File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));
            }
        }
 
        foreach (var dir in Directory.GetDirectories(srcPath))
        {
            CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)), enableExts);
        }
    }
 
    internal static void GetDirFileList(string dirPath, ref List<string> dirs, string[] enableExts, string subPathFrom="")
    {
        foreach (string path in Directory.GetFiles(dirPath))
        {
            if (enableExts.Contains(System.IO.Path.GetExtension(path)))
            {
                if(subPathFrom != ""){
                    dirs.Add(path.Substring(path.IndexOf(subPathFrom)));
                }else{
                    dirs.Add(path);
                }
            }
        }
 
        if (Directory.GetDirectories(dirPath).Length > 0)
        {
            foreach (string path in Directory.GetDirectories(dirPath))
            {
                GetDirFileList(path, ref dirs, enableExts, subPathFrom);
            }
        }
 
    }

    # endregion Helpers

}