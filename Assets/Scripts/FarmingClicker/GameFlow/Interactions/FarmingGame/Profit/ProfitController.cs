using Core.Message;
using FarmingClicker.GameFlow.Messages.Commands.Currency;
using FarmingClicker.GameFlow.Messages.Commands.Popups;
using InfiniteValue;

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
        [SerializeField, BoxGroup("Text")] private TMP_Text amountOfProfitText;
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;

        private InfVal amountOfProfit;
        
        public override void SetupData(IPopupData data)
        {
            if (data is not ProfitPopupData optionsPopupData) return;

            amountOfProfitText.text = InfValOperations.DisplayInfVal(optionsPopupData.Amount);
            exitButton.onClick.AddListener(ExitOptions);
            gameObject.SetActive(true);
            //MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(optionsPopupData.Amount));

        }

        private void ExitOptions()
        {
            gameObject.SetActive(false);
            MessageDispatcher.Instance.Send(new GiveRewardCoinsAnimationCommand(amountOfProfit));

            exitButton.onClick.RemoveAllListeners();
        }

    }
}