namespace Core.ScriptableObjectProvider
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    /// <summary>
    ///     Generic scriptable object provider.
    ///     Specify your target SO type when inheriting from it.
    ///     Provides one single method to retrieve object from internal object list by predicate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScriptableObjectProvider<T> : ObjectProvider<T> where T : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        ///     Just find all scriptable objects of given type in assets and fill list with results.
        /// </summary>
        [Button]
        protected void FindAllWithType()
        {
            var assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            for(int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if(asset != null)
                {
                    assets.Add(asset);
                }
            }

            providedObjects = assets;
        }
#endif
    }
}