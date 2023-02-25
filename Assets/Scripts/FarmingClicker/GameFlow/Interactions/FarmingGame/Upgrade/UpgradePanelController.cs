namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Upgrade
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmingClicker.Data.Popup;
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Workplaces;
    using Sirenix.OdinInspector;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;

    public class UpgradePanelController : PopupPanelBase, IMessageReceiver
    {
        
        [SerializeField, BoxGroup("Title")] private TMP_Text title;

        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button buyButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade1XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade5XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade10XButton;

        [SerializeField, BoxGroup("Statistics")]
        private UpgradeStatistics levelStatistic;        
        [SerializeField, BoxGroup("Statistics")]
        private UpgradeStatistics workersStatistic;
        [SerializeField, BoxGroup("Statistics")]
        private UpgradeStatistics valueStatistic;
        [SerializeField, BoxGroup("Statistics")]
        private UpgradeStatistics workingSpeedStatistic;
        [SerializeField, BoxGroup("Statistics")]
        private UpgradeStatistics movingSpeedStatistic;        
        [SerializeField, BoxGroup("Statistics")]
        private UpgradeStatistics loadStatistic;
        
        [SerializeField, BoxGroup("Statistic Components")]
        private UpgradeStatisticComponents levelStatisticComponents;        
        [SerializeField, BoxGroup("Statistic Components")]
        private UpgradeStatisticComponents workersStatisticComponents;
        [SerializeField, BoxGroup("Statistic Components")]
        private UpgradeStatisticComponents valueStatisticComponents;
        [SerializeField, BoxGroup("Statistic Components")]
        private UpgradeStatisticComponents workingSpeedStatisticComponents;
        [SerializeField, BoxGroup("Statistic Components")]
        private UpgradeStatisticComponents movingSpeedStatisticComponents;        
        [SerializeField, BoxGroup("Statistic Components")]
        private UpgradeStatisticComponents loadStatisticComponents;
        
        private WorkplaceController currentWorkplaceController;

        
        private void Awake()
        {

            ListenedTypes.Add(typeof(ChangeStatisticsOfUpgradeNotification));

            MessageDispatcher.Instance.RegisterReceiver(this);


        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        private void CloseGame()
        {
            RemoveListeners();
            gameObject.SetActive(false);

        }

        private void RemoveListeners()
        {
            exitButton.onClick.RemoveAllListeners();
            upgrade1XButton.onClick.RemoveAllListeners();
            upgrade5XButton.onClick.RemoveAllListeners();
            upgrade10XButton.onClick.RemoveAllListeners();
        }

        public override void SetupData(IPopupData data)
        {
            if (data is not UpgradeDisplayPopupData upgradeDisplayPopupData) return;
            
            title.text = upgradeDisplayPopupData.Title;
            currentWorkplaceController = upgradeDisplayPopupData.WorkplaceController;

            InitializeStatistics(upgradeDisplayPopupData);
            
            exitButton.onClick.AddListener(CloseGame);
            
            upgrade1XButton.onClick.AddListener(() =>{BuyUpgrade(1, upgradeDisplayPopupData.WorkplaceController);});
            upgrade5XButton.onClick.AddListener(() =>{BuyUpgrade(5, upgradeDisplayPopupData.WorkplaceController);});
            upgrade10XButton.onClick.AddListener(() =>{BuyUpgrade(10, upgradeDisplayPopupData.WorkplaceController);});
            
            gameObject.SetActive(true);
        }

        private void InitializeStatistics(UpgradeDisplayPopupData upgradeDisplayPopupData)
        {
            levelStatistic.InitializeStatistic(
                levelStatisticComponents.GetIcon(),
                levelStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetLevelIncrementedBy(1).ToString());
            
            workersStatistic.InitializeStatistic(
                workersStatisticComponents.GetIcon(),
                workersStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkerkersOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkerkersOfCurrentLevelIncrementedBy(1).ToString());
            
            
        }

        private void BuyUpgrade(int amount, WorkplaceController workplaceController)
        {
            workplaceController.BuyUpgrade(amount);
        }


        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case ChangeStatisticsOfUpgradeNotification changeStatisticsOfUpgradeNotification:
                {
                    //currentValueText.text = changeStatisticsOfUpgradeNotification.CurrentValueOfCroppedCurrency.ToString();
                    break;
                }
                
            }
        }
    }

    
}
