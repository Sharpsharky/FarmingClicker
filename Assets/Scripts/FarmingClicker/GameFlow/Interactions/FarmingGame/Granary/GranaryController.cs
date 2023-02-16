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
        private InfVal valX5;
        private InfVal valX10;
        private InfVal valX50;
        public int UpgradeLevel
        {
            get { return upgradeLevel; }
            set { upgradeLevel = value; }
        }
        public int NumberOfWorkers => numberOfWorkers;
        public InfVal CurrentCurrency => currentCurrency;
        public InfVal ValueOfTransportedCurrency => valueOfTransportedCurrency;
        
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