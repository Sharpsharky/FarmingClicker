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
        
        [SerializeField, BoxGroup("Main")] private TMP_Text title;
        [SerializeField, BoxGroup("Main")] private TMP_Text price;
        [SerializeField, BoxGroup("Main")] private Color inactiveButtonColor = new Color(1,1,1,0.65f);

        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button buyButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade1XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade5XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade10XButton;
        
        [SerializeField, BoxGroup("Button Images")] private Image buyButtonImage;
        [SerializeField, BoxGroup("Button Images")] private Image upgrade1XButtonImage;
        [SerializeField, BoxGroup("Button Images")] private Image upgrade5XButtonImage;
        [SerializeField, BoxGroup("Button Images")] private Image upgrade10XButtonImage;
        
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
        private int startingNumberOfIncrementedLevelsAfterDisplayingPopup = 1;
        
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

            InitializeStatistics(upgradeDisplayPopupData,startingNumberOfIncrementedLevelsAfterDisplayingPopup);
            
            exitButton.onClick.AddListener(CloseGame);
            
            upgrade1XButton.onClick.AddListener(() =>
            {
                InitializeStatistics(upgradeDisplayPopupData,1);
                ChangeColorOfButtons(1);
            });
            upgrade5XButton.onClick.AddListener(() =>
            {
                InitializeStatistics(upgradeDisplayPopupData,5);
                ChangeColorOfButtons(5);

            });
            upgrade10XButton.onClick.AddListener(() =>
            {
                InitializeStatistics(upgradeDisplayPopupData,10);
                ChangeColorOfButtons(10);

            });
            
            gameObject.SetActive(true);
        }

        private void InitializeStatistics(UpgradeDisplayPopupData upgradeDisplayPopupData, int levelsInfcementedByNumber)
        {
            levelStatistic.InitializeStatistic(
                levelStatisticComponents.GetIcon(),
                levelStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            workersStatistic.InitializeStatistic(
                workersStatisticComponents.GetIcon(),
                workersStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkerkersOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkerkersOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            valueStatistic.InitializeStatistic(
                valueStatisticComponents.GetIcon(),
                workersStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetValueOfLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetValueOfLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            workingSpeedStatistic.InitializeStatistic(
                workingSpeedStatisticComponents.GetIcon(),
                workersStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkingSpeedOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkingSpeedOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            movingSpeedStatistic.InitializeStatistic(
                movingSpeedStatisticComponents.GetIcon(),
                workersStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetMovingSpeedOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetMovingSpeedOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            loadStatistic.InitializeStatistic(
                loadStatisticComponents.GetIcon(),
                workersStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetLoadOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetLoadOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());

            price.text =
                upgradeDisplayPopupData.WorkplaceController.GetCostOfLevelIncrementedBy(levelsInfcementedByNumber).ToString();
        }

        private void ChangeColorOfButtons(int buttonToTurnOn)
        {
            upgrade1XButtonImage.color = inactiveButtonColor;
            upgrade5XButtonImage.color = inactiveButtonColor;
            upgrade10XButtonImage.color = inactiveButtonColor;

            switch (buttonToTurnOn)
            {
                case 1:
                {
                    upgrade1XButtonImage.color = Color.white;
                    break;
                }
                case 5:
                {
                    upgrade5XButtonImage.color = Color.white;

                    break;
                }
                default:
                {
                    upgrade10XButtonImage.color = Color.white;
                    break;
                }
            }
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
