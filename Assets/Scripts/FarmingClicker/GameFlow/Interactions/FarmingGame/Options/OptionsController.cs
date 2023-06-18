namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Options
{
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;

    public class OptionsController : PopupPanelBase
    {
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;

        public override void SetupData(IPopupData data)
        {
            //if (data is not OptionsPopupData optionsPopupData) return;

            gameObject.SetActive(true);
            exitButton.onClick.AddListener(ExitOptions);
        }

        private void ExitOptions()
        {
            gameObject.SetActive(false);
            exitButton.onClick.RemoveAllListeners();
        }

    }
}