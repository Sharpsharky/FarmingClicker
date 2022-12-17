namespace Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using Object = UnityEngine.Object;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public static class AssetsUtils
    {
#if UNITY_EDITOR

        /// <summary>
        ///     Get all instances of unity editor assset.
        ///     https://answers.unity.com/questions/1425758/how-can-i-find-all-instances-of-a-scriptable-objec.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            var a = new T[guids.Length];
            for(int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }

        public static List<T> FindAssets<T>() where T : Object
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            var result = new List<T>();
            foreach(string t in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(t);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if(asset != null)
                {
                    result.Add(asset);
                }
            }

            return result;
        }

        /// <summary>
        ///     Since 2020.x versions the "t:
        ///     <component>
        ///         " filter is broken and seems to not work properly with prefabs in assets.
        ///         So as fallback method this approach can be used. It's much slower, but it's effectively returning prefabs with
        ///         components.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindPrefabs<T>(string[] searchInFolders = null) where T : Object
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", searchInFolders);
            var result = new List<T>();
            foreach(string t in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(t);
                var asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if(asset != null && asset.GetComponent<T>() != null)
                {
                    result.Add(asset.GetComponent<T>());
                }
            }

            return result;
        }

        public static T FindFirstAsset<T>() where T : Object
        {
            var assets = FindAssets<T>();
            return assets.Count > 0 ? assets[0] : null;
        }

        public static List<T> TryGetUnityObjectsOfTypeFromPath<T>(string path) where T : Object
        {
            var result = new List<T>();
            string[] filePaths = Directory.GetFiles(path);

            if(filePaths.Length > 0)
            {
                for(int i = 0; i < filePaths.Length; i++)
                {
                    int stringIndex = filePaths[i].IndexOf("/Assets", StringComparison.Ordinal);
                    string newPath = filePaths[i].Substring(stringIndex + 1);
                    var loadedObject = AssetDatabase.LoadAssetAtPath(newPath, typeof(T));

                    if(!(loadedObject is T asset))
                    {
                        continue;
                    }

                    if(!result.Contains(asset))
                    {
                        result.Add(asset);
                    }
                }
            }

            return result;
        }

#endif
    }
}