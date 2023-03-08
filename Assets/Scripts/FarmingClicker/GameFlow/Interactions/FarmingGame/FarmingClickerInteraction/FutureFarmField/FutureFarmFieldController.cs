namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.FutureFarmField
{
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using Messages.Commands.Popups;
    using InfiniteValue;
    using UnityEngine;
    using UnityEngine.UI;
    using FarmsSpawnerManager;

    public class FutureFarmFieldController : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;

        private InfVal priceToBuyTheNextFarmField;
        private int timeOfFarmConstruction;
        private FarmCalculationData initialFarmCalculationData;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData, InfVal priceToBuyTheNextFarmField, int timeOfFarmConstruction)
        {
            this.priceToBuyTheNextFarmField = priceToBuyTheNextFarmField;
            this.timeOfFarmConstruction = timeOfFarmConstruction;
            this.initialFarmCalculationData = initialFarmCalculationData;
            SetPositionOfButton();
            upgradeButton.onClick.AddListener(OpenBuyNewFieldPopUp);
        }

        private void SetPositionOfButton()
        {
            Vector3 curPosOfUpgradeButton = gameObject.transform.position;
            curPosOfUpgradeButton.x = initialFarmCalculationData.XOfFirstUpgradeFarmFieldButton;
            upgradeButton.transform.parent.position = curPosOfUpgradeButton;
        }
        
        private void OpenBuyNewFieldPopUp()
        {
            var buyNewFieldPopupData = new BuyNewFieldPopupData(priceToBuyTheNextFarmField, timeOfFarmConstruction);
            MessageDispatcher.Instance.Send(new DisplayBuyNewFieldPanelCommand(buyNewFieldPopupData));
        }
        
    }
}
