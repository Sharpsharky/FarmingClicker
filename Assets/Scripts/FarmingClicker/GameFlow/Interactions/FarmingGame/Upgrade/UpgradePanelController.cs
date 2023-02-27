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
            InitializeStatistics(upgradeDisplayPopupData,buyMultipliers[0]);
            ChangeColorOfButtons(0);
            exitButton.onClick.AddListener(CloseGame);

            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                int index = i;
                upgradeButtons[i].onClick.AddListener(() =>
                {
                    InitializeStatistics(upgradeDisplayPopupData, buyMultipliers[index]);
                    ChangeColorOfButtons(index);
                });
            }
            
            gameObject.SetActive(true);
        }

        private void InitializeMultiplierButtonTexts()
        {
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                upgradeButtonTexts[i].text = $"{buyMultipliers[i]}x";
            }
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
                valueStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetValueOfLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetValueOfLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            workingSpeedStatistic.InitializeStatistic(
                workingSpeedStatisticComponents.GetIcon(),
                workingSpeedStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkingSpeedOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetWorkingSpeedOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            movingSpeedStatistic.InitializeStatistic(
                movingSpeedStatisticComponents.GetIcon(),
                movingSpeedStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetMovingSpeedOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetMovingSpeedOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());
            
            loadStatistic.InitializeStatistic(
                loadStatisticComponents.GetIcon(),
                loadStatisticComponents.GetTitle(),
                upgradeDisplayPopupData.WorkplaceController.GetLoadOfCurrentLevelIncrementedBy().ToString(),
                upgradeDisplayPopupData.WorkplaceController.GetLoadOfCurrentLevelIncrementedBy(levelsInfcementedByNumber).ToString());

            price.text =
                upgradeDisplayPopupData.WorkplaceController.GetCostOfLevelIncrementedBy(levelsInfcementedByNumber).ToString();
            
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() =>
            {
                BuyUpgrade(levelsInfcementedByNumber, upgradeDisplayPopupData.WorkplaceController);
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
                    //currentValueText.text = changeStatisticsOfUpgradeNotification.CurrentValueOfCroppedCurrency.ToString();
                    break;
                }
                
            }
        }
    }

    
}
