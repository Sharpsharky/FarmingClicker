namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FutureFarmField
{
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using Messages.Commands.Popups;
    using InfiniteValue;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class FutureFarmFieldController : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;

        private InfVal priceToBuyTheNextFarmField;
        private int timeOfFarmConstruction;
        
        public void Initialize(InfVal priceToBuyTheNextFarmField, int timeOfFarmConstruction)
        {
            this.priceToBuyTheNextFarmField = priceToBuyTheNextFarmField;
            this.timeOfFarmConstruction = timeOfFarmConstruction;

            upgradeButton.onClick.AddListener(OpenBuyNewFieldPopUp);
        }

        private void OpenBuyNewFieldPopUp()
        {
            var buyNewFieldPopupData = new BuyNewFieldPopupData(priceToBuyTheNextFarmField, timeOfFarmConstruction);
            MessageDispatcher.Instance.Send(new DisplayBuyNewFieldPanelCommand(buyNewFieldPopupData));
        }
        
    }
}
