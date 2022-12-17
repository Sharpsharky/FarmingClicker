namespace Utils.GameObjectUtils
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class GameObjectExtensions
    {
        public static List<T> GetComponents<T>(this List<GameObject> gameObjects)
            where T : Component
        {
            var resultList = new List<T>();
            foreach(var rootObject in gameObjects)
            {
                resultList.AddRange(rootObject.GetComponentsInChildren<T>());
            }

            return resultList;
        }
    }
}