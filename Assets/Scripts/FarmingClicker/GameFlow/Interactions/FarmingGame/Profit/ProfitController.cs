namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Profit
{
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ProfitController : PopupPanelBase
    {
        [SerializeField, BoxGroup("Text")] private TMP_Text amountOfProfit;
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