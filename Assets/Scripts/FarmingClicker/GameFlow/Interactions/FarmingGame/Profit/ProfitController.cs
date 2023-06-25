using Core.Message;
using FarmingClicker.GameFlow.Messages.Commands.Currency;
using FarmingClicker.GameFlow.Messages.Commands.Popups;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Profit
{
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Data;
    using FarmingClicker.Data.Popup;
    public class ProfitController : PopupPanelBase
    {
        [SerializeField, BoxGroup("Text")] private TMP_Text amountOfProfit;
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;

        public override void SetupData(IPopupData data)
        {
            if (data is not ProfitPopupData optionsPopupData) return;

            amountOfProfit.text = InfValOperations.DisplayInfVal(optionsPopupData.Amount);
            exitButton.onClick.AddListener(ExitOptions);
            gameObject.SetActive(true);
            MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(optionsPopupData.Amount));

        }

        private void ExitOptions()
        {
            gameObject.SetActive(false);
            exitButton.onClick.RemoveAllListeners();
        }

    }
}