namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces
{
    using System.Collections.Generic;
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using FarmsSpawnerManager;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    using Worker;
    using Messages.Commands.Popups;
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;

    public abstract class WorkplaceController : SerializedMonoBehaviour
    {
        [SerializeField] protected Button upgradeButton;
        [SerializeField] protected string title;

        protected FarmCalculationData initialFarmCalculationData;
        protected GameObject workerPrefab;
        protected WorkPlaceData workPlaceData;
        protected List<WorkerController> workerControllers = new List<WorkerController>(); 
        
        
        protected int upgradeLevel = 0;
        protected int numberOfWorkers = 0;
        protected InfVal currentCurrency = 0;
        protected InfVal valueOfTransportedCurrency = 0;
        protected InfVal valueOfCroppedCurrency = 0;
        protected int workingSpeed = 0;
        protected int movingSpeed = 0;

        public int UpgradeLevel
        {
            get { return upgradeLevel; }
            set { upgradeLevel = value; }
        }
        public int NumberOfWorkers => numberOfWorkers;
        public InfVal CurrentCurrency => currentCurrency;
        public InfVal ValueOfTransportedCurrency => valueOfTransportedCurrency;

        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, WorkPlaceData workPlaceData, GameObject workerPrefab)
        {
            Debug.Log($"Initialize WorkplaceController {gameObject.name}");
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.workPlaceData = workPlaceData;
            this.workerPrefab = workerPrefab;
            
            SetValueOfTransportedCurrency(valueOfTransportedCurrency);
            DisplayUpgradeButton(CalculatePositionOfButton());
            InitializeWorkers();
            InitializeWorkPlaceData();
        }

        private void InitializeWorkPlaceData()
        {
            SetValueOfCroppedCurrency(GetValueOfLevelIncrementedBy());
        }
        

        private void InitializeWorkers()
        {
            Debug.Log($"InitializeWorkers {gameObject.name}: {numberOfWorkers}");

            for (int i = 0; i < workPlaceData.numberOfWorkers; i++)
            {
                InitializeWorker();
            }
        }

        protected virtual WorkerController InitializeWorker()
        {

            if (workerPrefab == null) return null;
            
            GameObject newWorker = Instantiate(workerPrefab, initialFarmCalculationData.StartingPoint, Quaternion.identity);
            var newWorkerController = newWorker.GetComponent<WorkerController>();
            workerControllers.Add(newWorkerController);

            Debug.Log("InitializeWorker");
            
            return newWorkerController;
            //Now initialize the Worker.
            /*
            newWorkerController.Initialize(new List<WorkplaceController>(initialFarmCalculationData.FarmFieldControllers), 
                initialFarmCalculationData.StartingPoint, initialFarmCalculationData.YOfFirstStop, 
                initialFarmCalculationData.DistanceBetweenStops, initialFarmCalculationData.NumberOfStops, 
                initialFarmCalculationData.YOfGarage, 
                initialFarmCalculationData.GranaryControllers[0].gameObject.transform.position);*/

        }

        protected virtual Vector3 CalculatePositionOfButton()
        {
            return transform.position;
        }
        
        protected InfVal SetValueOfTransportedCurrency(InfVal valueOfTransportedCurrency)
        {
            this.valueOfTransportedCurrency = valueOfTransportedCurrency;

            return valueOfTransportedCurrency;
        }
        protected InfVal CalculateValueOfCroppedCurrency(int upgradeLevel)
        {
            return 1 * upgradeLevel;
        }
        protected InfVal SetValueOfCroppedCurrency(InfVal valueOfCroppedCurrency)
        {
            this.valueOfCroppedCurrency = valueOfCroppedCurrency;

            return valueOfCroppedCurrency;
        }
        protected void DisplayUpgradeButton(Vector3 buttonPos)
        {
            upgradeButton.onClick.AddListener(DisplayUpgrade);
            upgradeButton.gameObject.transform.parent.position = buttonPos;
        }
        
        protected void DisplayUpgrade()
        {
            UpgradeDisplayPopupData data = new UpgradeDisplayPopupData(this, I2.Loc.LocalizationManager.GetTranslation(title));
            
            MessageDispatcher.Instance.Send(new DisplayUpgradePanelCommand(data));
        }

        public void BuyUpgrade(int numberOfBoughtLevels)
        {
            upgradeLevel += numberOfBoughtLevels;
            Debug.Log($"upgradeLevel: {upgradeLevel}");
            valueOfCroppedCurrency = SetValueOfCroppedCurrency(GetValueOfLevelIncrementedBy());
            Debug.Log($"valueOfCroppedCurrency: {valueOfCroppedCurrency}");

            MessageDispatcher.Instance.Send(
                new ChangeStatisticsOfUpgradeNotification(valueOfCroppedCurrency));
        }



        public int GetLevelIncrementedBy(int i = 0)
        {
            return upgradeLevel + i;
        }
        
        public int GetWorkerkersOfCurrentLevelIncrementedBy(int i = 0)
        {
            int incrementedUpgradeLevel = upgradeLevel + i;
            return incrementedUpgradeLevel;
        }

        public InfVal GetValueOfLevelIncrementedBy(int i = 0)
        {
            InfVal collectedValue = 0;

            for (int j = 0; j < i + 1; j++)
            {
                collectedValue += 1.67f * (upgradeLevel+j+1) * 1;
            }
            
            return collectedValue;
        }

        public InfVal GetCostOfLevelIncrementedBy(int i = 0)
        {
            float a = 3.67f;
            InfVal b = Mathf.Pow(1.07f, upgradeLevel);
            InfVal c = Mathf.Pow(1.07f, i)-1;
            float d = 1.07f - 1;

            InfVal finalCost = a * ((b * c) / d);

            return finalCost;
        }
        
        public int GetWorkingSpeedOfCurrentLevelIncrementedBy(int i = 0)
        {
            return 1;
        }

        public int GetMovingSpeedOfCurrentLevelIncrementedBy(int i = 0)
        {
            return 1;
        }
        
        public InfVal GetLoadOfCurrentLevelIncrementedBy(int i = 0)
        {
            return 100 * upgradeLevel;
        }
        
    }
}