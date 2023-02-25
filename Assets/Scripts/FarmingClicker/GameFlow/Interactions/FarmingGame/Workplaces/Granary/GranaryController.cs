
namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.Granary
{
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using Messages.Commands.Popups;
    using InfiniteValue;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class GranaryController : WorkplaceController
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private string title;
        
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfTransportedCurrency = 0;

        public int UpgradeLevel
        {
            get { return upgradeLevel; }
            set { upgradeLevel = value; }
        }
        public int NumberOfWorkers => numberOfWorkers;
        public InfVal CurrentCurrency => currentCurrency;
        public InfVal ValueOfTransportedCurrency => valueOfTransportedCurrency;

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

    }
}