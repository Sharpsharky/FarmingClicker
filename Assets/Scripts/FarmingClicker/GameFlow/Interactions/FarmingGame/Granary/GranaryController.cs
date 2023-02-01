namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Granary
{
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine.UI;
    using UnityEngine;
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using General;
    using Messages.Commands.Popups;
    public class GranaryController : SerializedMonoBehaviour, IFarmWorkerControllable
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private string title;

        
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfTransportedCurrency = 0;
        
        public void Initialize(int upgradeLevel, int numberOfWorkers, InfVal currentCurrency, InfVal valueOfTransportedCurrency)
        {
            this.upgradeLevel = upgradeLevel;
            this.numberOfWorkers = numberOfWorkers;
            this.currentCurrency = currentCurrency;
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
            UpgradeDisplayPopupData data = new UpgradeDisplayPopupData(this, title, currentCurrency);
            
            MessageDispatcher.Instance.Send(new DisplayUpgradePanelCommand(data));

        }
        
        public void BuyUpgrade(int amount)
        {
            upgradeLevel += amount;
        }
    }
}