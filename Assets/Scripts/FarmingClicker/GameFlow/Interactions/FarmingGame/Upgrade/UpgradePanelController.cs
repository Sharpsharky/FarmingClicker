using FarmingClicker.Dialogue.DialogueDataTypes;
using FarmingClicker.Dialogue.DialoguePanelControllers;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Upgrade
{
    public class UpgradePanelController : PopupPanelBase
    {
        
        [SerializeField, BoxGroup("Title")] private TMP_Text title;

        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade1XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade5XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade10XButton;
        
        [SerializeField, BoxGroup("Statistics")] private TMP_Text currentValue;


        public void Open()
        {
            gameObject.SetActive(true);
        }

        private void CloseGame()
        {
            gameObject.SetActive(false);

        }

        public override void SetupData(IPopupData data)
        {
            
        }
    }
}
