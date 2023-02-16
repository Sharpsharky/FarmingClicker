using Core.Message;
using FarmingClicker.Data.Popup;
using FarmingClicker.GameFlow.Interactions.FarmingGame.General;
using FarmingClicker.GameFlow.Messages.Commands.Popups;
using InfiniteValue;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop
{
    public class FarmShopController : SerializedMonoBehaviour, IFarmWorkerControllable
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private string title;
        
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfTransportedCurrency = 0;
        private InfVal valX5;
        private InfVal valX10;
        private InfVal valX50;
        public void Initialize(int upgradeLevel, int numberOfWorkers, InfVal currentCurrency,
            InfVal valueOfTransportedCurrency, InfVal valX5, InfVal valX10, InfVal valX50)
        {
            this.upgradeLevel = upgradeLevel;
            this.numberOfWorkers = numberOfWorkers;
            this.currentCurrency = currentCurrency;
            this.valX5 = valX5;
            this.valX10 = valX10;
            this.valX50 = valX50;
            SetValueOfTransportedCurrency(valueOfTransportedCurrency);
            DisplayUpgradeButton(transform.position);
        }
        
        public InfVal SetValueOfTransportedCurrency(InfVal valueOfTransportedCurrency)
        {
            this.valueOfTransportedCurrency = valueOfTransportedCurrency;

            return valueOfTransportedCurrency;
        }

        public void DisplayUpgradeButton(Vector3 buttonPos)
        {
            upgradeButton.gameObject.transform.position = buttonPos;
            upgradeButton.onClick.AddListener(DisplayUpgrade);
        }

        private void DisplayUpgrade()
        {
            UpgradeDisplayPopupData data = new UpgradeDisplayPopupData(this, title, currentCurrency, 
                valX5, valX10, valX50);
            
            MessageDispatcher.Instance.Send(new DisplayUpgradePanelCommand(data));

        }

        public void BuyUpgrade(int amount)
        {
            upgradeLevel += amount;
        }
    }
}