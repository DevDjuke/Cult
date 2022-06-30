using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Service.Assets
{
    public static class AssetBundleLoadService
    {
        private static AssetBundle currentAssetBundle;
        
        public static AssetBundle GetCurrentAssetBundle()
        {
            return currentAssetBundle;
        }

        public static AssetBundle GetAssetBundle(string name)
        {
            if(currentAssetBundle != null)
            {
                if (!currentAssetBundle.name.Equals(name))
                {
                    UnloadCurrentAssetBundle();
                    LoadAssetBundle(name);
                }
                else
                {
                    return GetCurrentAssetBundle();
                }
            }
            else
            {
                LoadAssetBundle(name);
            }

            return GetCurrentAssetBundle();
        }

        public static void LoadAssetBundle(string name)
        {
            string path = Path.Combine(Application.dataPath, "Bundles" , name);


            currentAssetBundle = AssetBundle.LoadFromFile(path);
            if (currentAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }
        }

        public static void UnloadCurrentAssetBundle()
        {
            if(currentAssetBundle != null)
            {
                currentAssetBundle.Unload(true);
            }
        }

        public static void UnloadAssetBundle(string name)
        {
            if(currentAssetBundle != null && currentAssetBundle.name.Equals(name))
            {
                currentAssetBundle.Unload(true);
            }
        }
    }
}
