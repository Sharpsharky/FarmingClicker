using System.Diagnostics;

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
        [SerializeField, BoxGroup("Main")] private List<int> buyMultipliers = new List<int>();
        [SerializeField, BoxGroup("Main")] private float alphaOfInactiveText = 0.65f;

        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button buyButton;
        [SerializeField, BoxGroup("Buttons")] private List<Button> upgradeButtons = new List<Button>();
        [SerializeField, BoxGroup("Buttons")] private List<TMP_Text> upgradeButtonTexts = new List<TMP_Text>();
        [SerializeField, BoxGroup("Buttons")] private List<Image> upgradeButtonImages = new List<Image>();
        [SerializeField, BoxGroup("Buttons")] private List<GameObject> upgradeButtonReflections = new List<GameObject>();
        
        [SerializeField, BoxGroup("Button Images")] private Image buyButtonImage;
        
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
        private int currentMultiplyButtonPressed = -1;

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
            foreach (var upgradeButton in upgradeButtons)
            {
                upgradeButton.onClick.RemoveAllListeners();
            }
        }

        public override void SetupData(IPopupData data)
        {
            if (data is not UpgradeDisplayPopupData upgradeDisplayPopupData) return;
            
            title.text = upgradeDisplayPopupData.Title;
            currentWorkplaceController = upgradeDisplayPopupData.WorkplaceController;
            InitializeMultiplierButtonTexts();
            ChangeColorOfButtons(0);
            exitButton.onClick.AddListener(CloseGame);

            InitializeStatistics(buyMultipliers[0]);

            InitializeButtons(upgradeDisplayPopupData);
            
            gameObject.SetActive(true);
        }

        private void InitializeButtons(UpgradeDisplayPopupData upgradeDisplayPopupData)
        {
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                int index = i;
                upgradeButtons[i].onClick.AddListener(() =>
                {
                    InitializeStatistics(buyMultipliers[index]);
                    ChangeColorOfButtons(index);
                });
            }
        }
        
        private void InitializeMultiplierButtonTexts()
        {
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                upgradeButtonTexts[i].text = $"{buyMultipliers[i]}x";
            }
        }
        
        
        private void InitializeStatistics(int levelsIncrementedByNumber)
        {
            Debug.Log($"levelsIncrementedByNumber: {levelsIncrementedByNumber}");
            Debug.Log($"currentWorkplaceController.GetLoadOfCurrentLevelIncrementedBy(): {currentWorkplaceController.GetLoadOfCurrentLevelIncrementedBy()}");
            levelStatistic.InitializeStatistic(
                levelStatisticComponents.GetIcon(),
                $"{levelStatisticComponents.GetTitle()}:",
                currentWorkplaceController.GetLevelIncrementedBy().ToString(),
                currentWorkplaceController.GetLevelIncrementedBy(levelsIncrementedByNumber).ToString());
            
            workersStatistic.InitializeStatistic(
                workersStatisticComponents.GetIcon(),
                $"{workersStatisticComponents.GetTitle()}:",
                currentWorkplaceController.GetWorkersOfCurrentLevelIncrementedBy().ToString(),
                currentWorkplaceController.GetWorkersOfCurrentLevelIncrementedBy(levelsIncrementedByNumber).ToString());
            
            valueStatistic.InitializeStatistic(
                valueStatisticComponents.GetIcon(),
                $"{valueStatisticComponents.GetTitle()}:",
                currentWorkplaceController.GetValueOfLevelIncrementedBy().ToString(),
                currentWorkplaceController.GetValueOfLevelIncrementedBy(levelsIncrementedByNumber).ToString());
            
            workingSpeedStatistic.InitializeStatistic(
                workingSpeedStatisticComponents.GetIcon(),
                $"{workingSpeedStatisticComponents.GetTitle()}:",
                currentWorkplaceController.GetWorkingSpeedOfCurrentLevelIncrementedBy().ToString(),
                currentWorkplaceController.GetWorkingSpeedOfCurrentLevelIncrementedBy(levelsIncrementedByNumber).ToString());
            
            movingSpeedStatistic.InitializeStatistic(
                movingSpeedStatisticComponents.GetIcon(),
                $"{movingSpeedStatisticComponents.GetTitle()}:",
                currentWorkplaceController.GetMovingSpeedOfCurrentLevelIncrementedBy().ToString(),
                currentWorkplaceController.GetMovingSpeedOfCurrentLevelIncrementedBy(levelsIncrementedByNumber).ToString());
            
            loadStatistic.InitializeStatistic(
                loadStatisticComponents.GetIcon(),
                $"{loadStatisticComponents.GetTitle()}:",
                currentWorkplaceController.GetLoadOfCurrentLevelIncrementedBy().ToString(),
                currentWorkplaceController.GetLoadOfCurrentLevelIncrementedBy(levelsIncrementedByNumber).ToString());

            price.text =
                currentWorkplaceController.GetCostOfLevelIncrementedBy(levelsIncrementedByNumber).ToString();
            
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() =>
            {
                BuyUpgrade(levelsIncrementedByNumber, currentWorkplaceController);
            });

        }
        

        private void ChangeTextColorToCertainAlpha(TMP_Text targetText, float alphaLevel)
        {
            Color originalColor = targetText.color;
            originalColor.a = alphaLevel;
            targetText.color = originalColor;
        }

        private void ChangeColorOfButtons(int buttonToTurnOnIndex)
        {
            if(buttonToTurnOnIndex == currentMultiplyButtonPressed) return;
            
            currentMultiplyButtonPressed = buttonToTurnOnIndex;
            
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                if (i == buttonToTurnOnIndex)
                {
                    Debug.Log("ChangeColor");
                    upgradeButtonImages[i].color = Color.white;
                    ChangeTextColorToCertainAlpha(upgradeButtonTexts[i], 1);
                    upgradeButtonReflections[i].SetActive(true);
                }
                else
                {
                    upgradeButtonImages[i].color = inactiveButtonColor;
                    ChangeTextColorToCertainAlpha(upgradeButtonTexts[i], alphaOfInactiveText);
                    upgradeButtonReflections[i].SetActive(false);
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
                    InitializeStatistics(buyMultipliers[currentMultiplyButtonPressed]);
                    break;
            }
                
            }
        }
    }

    
}
