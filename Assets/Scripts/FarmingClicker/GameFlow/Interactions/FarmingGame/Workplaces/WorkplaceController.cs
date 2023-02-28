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
        [SerializeField] private Button upgradeButton;
        [SerializeField] private string title;

        private FarmCalculationData initialFarmCalculationData;
        private GameObject workerPrefab;
        private List<WorkPlaceData> workPlaceDataList = new List<WorkPlaceData>();
        private List<WorkerController> workerControllers = new List<WorkerController>(); 
        
        
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

        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkPlaceData> workPlaceDataList, GameObject workerPrefab)
        {
            Debug.Log($"Initialize WorkplaceController {gameObject.name}");
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.workPlaceDataList = new List<WorkPlaceData>(workPlaceDataList);
            this.workerPrefab = workerPrefab;
            
            SetValueOfTransportedCurrency(valueOfTransportedCurrency);
            DisplayUpgradeButton(transform.position);
            InitializeWorkers();
        }

        private void InitializeWorkers()
        {
            for (int i = 0; i < numberOfWorkers; i++)
            {
                InitializeWorker();
            }
        }

        protected virtual void InitializeWorker()
        {

            GameObject newWorker = Instantiate(workerPrefab, initialFarmCalculationData.StartingPoint, Quaternion.identity);
            var newWorkerController = newWorker.GetComponent<WorkerController>();
            workerControllers.Add(newWorkerController);

            
            //Now initialize the Worker.
            
            /*newWorkerController.Initialize(initialFarmCalculationData.FarmFieldControllers, 
                initialFarmCalculationData.StartingPoint, initialFarmCalculationData.YOfFirstStop, 
                initialFarmCalculationData.DistanceBetweenStops, initialFarmCalculationData.NumberOfStops, 
                initialFarmCalculationData.YOfGarage, 
                initialFarmCalculationData.GranaryController.gameObject.transform.position);*/
            
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
            upgradeButton.gameObject.transform.position = buttonPos;
            upgradeButton.onClick.AddListener(DisplayUpgrade);
        }
        
        protected void DisplayUpgrade()
        {
            UpgradeDisplayPopupData data = new UpgradeDisplayPopupData(this, title);
            
            MessageDispatcher.Instance.Send(new DisplayUpgradePanelCommand(data));
        }

        public void BuyUpgrade(int numberOfBoughtLevels)
        {
            upgradeLevel += numberOfBoughtLevels;

            var currentValueOfCroppedCurrency = SetValueOfTransportedCurrency(
                CalculateValueOfCroppedCurrency(UpgradeLevel));

            MessageDispatcher.Instance.Send(
                new ChangeStatisticsOfUpgradeNotification(currentValueOfCroppedCurrency));
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