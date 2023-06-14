using FarmingClicker.Data;

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

        private InfVal priceToBuyTheNextFarmField = new InfVal(0, InGameData.InfValPrecision);
        private int timeOfFarmConstruction;
        private FarmCalculationData initialFarmCalculationData;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData)
        {
            SetDataForFutureFarmField(priceToBuyTheNextFarmField,timeOfFarmConstruction);
            this.initialFarmCalculationData = initialFarmCalculationData;
            SetPositionOfButton();
            upgradeButton.onClick.AddListener(OpenBuyNewFieldPopUp);
        }

        public void PutFutureFarmInNewPosition()
        {
            var posOfFutureFarm = transform.position;
            posOfFutureFarm.y -= initialFarmCalculationData.DistanceBetweenStops;
            transform.position = posOfFutureFarm;
        }

        public void SetDataForFutureFarmField(InfVal priceToBuyTheNextFarmField, int timeOfFarmConstruction)
        {
            this.priceToBuyTheNextFarmField = priceToBuyTheNextFarmField;
            this.timeOfFarmConstruction = timeOfFarmConstruction;
        }

        public InfVal GetPrice()
        {
            return priceToBuyTheNextFarmField;
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
