namespace Utils.ScriptableObjectSingleton
{
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public abstract class SingletonScriptableObject<T> : SerializedScriptableObject
        where T : ScriptableObject
    {
        #region Private Variables

        private static T instance;

        #endregion

        #region Public Variables

        public static T Instance
        {
            get
            {
                if(!instance)
                {
                    instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                }

                return instance;
            }
        }

        #endregion
    }
}