namespace FarmingClicker.GameFlow.Messages
{
    
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Utils.ScenesHelper;
    public record LoadSceneCommand : Command
    {
        [SerializeField] private bool overrideSceneProgramatically;
        [HideIf(nameof(overrideSceneProgramatically))] [SerializeField]
        [ValueDropdown(nameof(GetAvailableScenes))]
        protected string sceneName;

        public List<string> DataToPass = new List<string>();

        public string SceneName
        {
            get => sceneName;
            set => sceneName = value;
        }

        public LoadSceneCommand(bool unload, string sceneName, List<string> dataToPass)
        {
            Unload = unload;
            this.sceneName = sceneName;
            this.DataToPass = dataToPass;
        }

        [field: SerializeField] public bool Unload { get; set; }
        
        #region Editor
        private List<string> GetAvailableScenes()
        {
            return ScenesHelper.GetAvailableScenesRawList();
        }
        #endregion
    }
}