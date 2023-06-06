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

        protected WorkerProperties workerProperties = new WorkerProperties();
        protected InfVal currentCurrency = 0;
        
        #region Getters and Setters
        public WorkerProperties WorkerProperties => workerProperties;

        public InfVal CurrentCurrency
        {
            get { return currentCurrency; }
            set { currentCurrency = value; }
        }
        
        #endregion
        
        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, WorkPlaceData workPlaceData, 
            GameObject workerPrefab)
        {
            Debug.Log($"Initialize WorkplaceController {initialFarmCalculationData}");
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.workPlaceData = workPlaceData;
            this.workerPrefab = workerPrefab;
            
            workerProperties.ChangeUpgradeLevel(1);
            
            DisplayUpgradeButton(CalculatePositionOfButton());
            InitializeWorkers();
        }

        private void InitializeWorkers()
        {
            for (int i = 0; i < workPlaceData.numberOfWorkers; i++)
            {
                InitializeWorker();
            }
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
            workerProperties.ChangeUpgradeLevel(numberOfBoughtLevels);

            MessageDispatcher.Instance.Send(
                new ChangeStatisticsOfUpgradeNotification(workerProperties.CroppedCurrency));
        }

    }
}