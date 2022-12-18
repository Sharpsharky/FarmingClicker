namespace FarmingClicker.GameFlow.Messages.Notifications.Scene
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Utils.ScenesHelper;

    [Serializable]
    public record SceneLoadedNotification : GameFlowMessage
    {        
        [ValueDropdown(nameof(GetAvailableScenes))]
        [SerializeField] public string SceneName;

        [SerializeField] public List<string> DataToPass;
       
        public SceneLoadedNotification(string sceneName, List<string> dataToPass)
        {
            SceneName = sceneName;
            DataToPass = dataToPass;
        }
        #region Editor
        private List<string> GetAvailableScenes()
        {
            return ScenesHelper.GetAvailableScenesRawList();
        }
        
        public virtual bool Equals(SceneLoadedNotification other)
        {
            return other.SceneName == SceneName;
        }

        public override int GetHashCode()
        {
            return SceneName.GetHashCode();
        }

        #endregion
    }
}
