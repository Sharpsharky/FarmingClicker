using System.Threading;
using FarmingClicker.GameFlow.Interactions.FarmingGame.GlobalData;
using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;

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
    using CurrencyFarm;
    using Messages.Commands.Currency;
    
    public abstract class WorkplaceController : SerializedMonoBehaviour
    {
        [SerializeField] protected Button upgradeButton;
        [SerializeField] protected string title;
        
        protected InitialWorkerProperties initialWorkerProperties;
        protected FarmCalculationData initialFarmCalculationData;
        protected GameObject workerPrefab;
        protected WorkPlaceData workPlaceData;
        protected List<WorkerController> workerControllers = new List<WorkerController>();

        protected WorkerProperties workerProperties = new WorkerProperties();
        protected InfVal currentCurrency = 0;

        protected WorkerManagerStatistics workerManagerSelected;
        protected List<WorkerManagerStatistics> workerManagersReadyToSelect = new List<WorkerManagerStatistics>();
        protected List<StatisticsTypes> statisticsTypes = new List<StatisticsTypes>();

        private int managersForSelectionNumber = 3;
        
        private int level0ManagerProbability = 55;
        private int level1ManagerProbability = 30;
        private int level2ManagerProbability = 19;
        private int level3ManagerProbability = 1;

        #region Getters and Setters
        public WorkerProperties WorkerProperties => workerProperties;
        public WorkerManagerStatistics WorkerManagerSelected => workerManagerSelected;

        public InfVal CurrentCurrency
        {
            get { return currentCurrency; }
            set { currentCurrency = value; }
        }
        
        #endregion
        
        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, WorkPlaceData workPlaceData, 
            GameObject workerPrefab, InitialWorkerProperties initialWorkerProperties, List<StatisticsTypes> statisticsTypes)
        {
            Debug.Log($"Initialize WorkplaceController {initialFarmCalculationData}");
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.workPlaceData = workPlaceData;
            this.workerPrefab = workerPrefab;
            this.initialWorkerProperties = initialWorkerProperties;
            this.statisticsTypes = new List<StatisticsTypes>(statisticsTypes);

            SetWorkerManagerStatistics(workPlaceData.workerManagerStatistics);
            
            workerProperties.SetInitialProperties(initialWorkerProperties);
            workerProperties.ChangeUpgradeLevel(workPlaceData.upgradeLevel);
            currentCurrency = workPlaceData.GetCurrentCurrency();
            DrawManagersForSelection();
            
            DisplayUpgradeButton(CalculatePositionOfButton());
            InitializeWorkers();
           
        }

        public WorkPlaceData GetSavingData()
        {
            Debug.Log($"workerProperties.UpgradeLevel: {workerProperties.UpgradeLevel},currentCurrency: {currentCurrency}");
            return new (workerProperties.UpgradeLevel, currentCurrency, workerManagerSelected);
        }

        public List<WorkerManagerStatistics> GetWorkerManagers()
        {
            return new List<WorkerManagerStatistics>(workerManagersReadyToSelect);
        }

        public void SetWorkerManagerStatistics(WorkerManagerStatistics workerManagerStatistics)
        {
            workerManagerSelected = workerManagerStatistics;
        }
        
        private void InitializeWorkers()
        {
            for (int i = 0; i < workerProperties.NumberOfWorkers; i++)
            {
                InitializeWorker();
            }
        }

        private void DrawManagersForSelection()
        {
            workerManagersReadyToSelect = new List<WorkerManagerStatistics>();
            
            for (int i = 0; i < managersForSelectionNumber; i++)
            {
                var workerManagerStatistic = DrawRandomManager();
                workerManagersReadyToSelect.Add(workerManagerStatistic);
            }
        }

        private WorkerManagerStatistics DrawRandomManager()
        {
            int levelOfManager = DrawRandomLevelOfManager();
            StatisticsTypes randomStatisticType = GetRandomStatisticType();
            int faceId = Random.Range(0, GlobalDataHolder.instance.spritesForManagersFaces.Count);

            return new WorkerManagerStatistics(levelOfManager, randomStatisticType, faceId);
        }
        
        private int DrawRandomLevelOfManager()
        {
            int maxRand = level0ManagerProbability + level1ManagerProbability + level2ManagerProbability +
                          level3ManagerProbability;
            
            float rand = Random.Range(0, maxRand);
            
            if (rand < level0ManagerProbability) return 0;
            else if (rand < level0ManagerProbability + level1ManagerProbability) return 1; 
            else if (rand < level0ManagerProbability + level1ManagerProbability + level2ManagerProbability) return 2;
            else return 3;
        }

        private StatisticsTypes GetRandomStatisticType()
        {
            int rand = Random.Range(0, statisticsTypes.Count);
            return statisticsTypes[rand];
        }
        
        protected virtual WorkerController InitializeWorker()
        {

            if (workerPrefab == null) return null;
            
            GameObject newWorker = Instantiate(workerPrefab, initialFarmCalculationData.StartingPoint, 
                Quaternion.identity);
            var newWorkerController = newWorker.GetComponent<WorkerController>();
            workerControllers.Add(newWorkerController);

            Debug.Log("InitializeWorker");
            
            return newWorkerController;
        }

        protected virtual Vector3 CalculatePositionOfButton()
        {
            return transform.position;
        }
        
        
        protected void DisplayUpgradeButton(Vector3 buttonPos)
        {
            upgradeButton.onClick.AddListener(DisplayUpgrade);
            upgradeButton.gameObject.transform.parent.position = buttonPos;
        }
        
        protected void DisplayUpgrade()
        {
            UpgradeDisplayPopupData data = new UpgradeDisplayPopupData(this, 
                I2.Loc.LocalizationManager.GetTranslation(title));
            
            MessageDispatcher.Instance.Send(new DisplayUpgradePanelCommand(data));
        }

        public void BuyUpgrade(int numberOfBoughtLevels)
        {
            InfVal cost = workerProperties.CalculateCostOfNextLevel(numberOfBoughtLevels);
            if (cost > CurrencyFarmManger.GetCurrentCurrency()) return;
            
            MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(-cost));
            
            workerProperties.ChangeUpgradeLevel(numberOfBoughtLevels);
            
            MessageDispatcher.Instance.Send(
                new ChangeStatisticsOfUpgradeNotification(workerProperties.CroppedCurrency));
        }

    }
}