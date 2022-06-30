using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets.Editor
{
    public class AssetBundleCreatore : MonoBehaviour
    {
        [MenuItem("Assets/Build Asset Bundles")]
        static void BuildABs()
        {
            string path = "Assets/Bundles";
            if(!AssetDatabase.IsValidFolder(path)) Directory.CreateDirectory(path);
            
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
}


