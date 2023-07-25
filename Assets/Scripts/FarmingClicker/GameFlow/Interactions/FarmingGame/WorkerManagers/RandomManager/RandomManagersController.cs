using FarmingClicker.Data.Popup;
using FarmingClicker.Dialogue.DialogueDataTypes;
using FarmingClicker.Dialogue.DialoguePanelControllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers.RandomManager
{
    public class RandomManagersController : PopupPanelBase
    {
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        
        public override void SetupData(IPopupData data)
        {
            if (data is not RandomManagersPopupData randomManagersPopupData) return;
            
            exitButton.onClick.AddListener(Exit);
            
            gameObject.SetActive(true);
        }

        private void Exit()
        {
            exitButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
        
    }
}