using UnityEngine;
using System.Collections;
using UnityEditor;

public static class ExportPackage
{
    [MenuItem("Export/Export with tags")]
    public static void ExportAll()
    {
        //string[] projectContent = new string[] { "Assets", "ProjectSettings/TagManager.asset", "ProjectSettings/InputManager.asset", "ProjectSettings/ProjectSettings.asset" };
        string[] projectContent = new string[] { "Assets", "ProjectSettings/TagManager.asset" };
        AssetDatabase.ExportPackage(projectContent, "MinimalShooting.unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
        Debug.Log("Project Exported");
    }
}
