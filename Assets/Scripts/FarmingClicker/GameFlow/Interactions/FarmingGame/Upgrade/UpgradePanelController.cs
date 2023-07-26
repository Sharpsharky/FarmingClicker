using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
using Unity.VisualScripting;

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
    using Data;
    using WorkerManagers;
    using Messages.Commands.Popups;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Managers;
    
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
        
        [SerializeField, BoxGroup("Manager")] private WorkerManagerFaceController workerManagerFaceController;
        
        [SerializeField, BoxGroup("Button Images")] private Image buyButtonImage;
        
        [SerializeField, BoxGroup("Statistics 2")]
        private List<StatisticsTypes> defaultStatistics = new List<StatisticsTypes>();
        [SerializeField, BoxGroup("Statistics 2")]
        private Dictionary<StatisticsTypes, UpgradeStatistics> upgradeStatisticsMap;
        [SerializeField, BoxGroup("Statistics 2")]
        private Dictionary<StatisticsTypes, UpgradeStatisticComponents> upgradeStatisticComponentsMap;
        
        private WorkplaceController currentWorkplaceController;
        private int startingNumberOfIncrementedLevelsAfterDisplayingPopup = 1;
        private int currentMultiplyButtonPressed = -1;

        private Action DrawNewRandomManagers;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();

        private void Awake()
        {
            ListenedTypes.Add(typeof(ChangeStatisticsOfUpgradeNotification));
            ListenedTypes.Add(typeof(NewWorkerManagerSelectedNotification));

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
            workerManagerFaceController.FaceButton.onClick.RemoveAllListeners();
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
            
            DrawNewRandomManagers = null;
            DrawNewRandomManagers += currentWorkplaceController.ReloadManagers;

            InitializeMultiplierButtonTexts();
            ChangeColorOfButtons(0);
            exitButton.onClick.AddListener(CloseGame);

            InitializeStatistics(buyMultipliers[0]);

            InitializeButtons(upgradeDisplayPopupData);

            SetUpWorkerManager();
            
            gameObject.SetActive(true);
        }

        private void SetUpWorkerManager()
        {
            workerManagerFaceController.FaceButton.onClick.AddListener(TurnOnManagerSelection);
            InitializeManager(currentWorkplaceController.WorkerManagerSelected);
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
            Debug.Log($"CalculateCroppedCurrency: levelsIncrementedByNumber: {levelsIncrementedByNumber}" );

            foreach (var upgradeStatistic in upgradeStatisticsMap)
            {
                upgradeStatistic.Value.gameObject.SetActive(false);
            }

            foreach (var statisticsType in defaultStatistics)
            {
                LoadStatistic(statisticsType, levelsIncrementedByNumber);
            }
            
            foreach (var statisticsType in currentWorkplaceController.StatisticsTypes)
            {
                LoadStatistic(statisticsType, levelsIncrementedByNumber);
            }
            
            SetupBuyButton(levelsIncrementedByNumber);
        }

        private void LoadStatistic(StatisticsTypes statisticsType, int levelsIncrementedByNumber)
        {
            var upgradeStatisticComponent = upgradeStatisticComponentsMap[statisticsType];
                
            upgradeStatisticsMap[statisticsType].InitializeStatistic(
                upgradeStatisticComponent.GetIcon(),
                $"{upgradeStatisticComponent.GetTitle()}:",
                currentWorkplaceController.WorkerProperties.GetValueOfStatistic(statisticsType),
                currentWorkplaceController.WorkerProperties.
                    GetValueOfStatistic(statisticsType, levelsIncrementedByNumber)
            );
            
            upgradeStatisticsMap[statisticsType].gameObject.SetActive(true);
        }
        
        
        private void SetupBuyButton(int levelsIncrementedByNumber)
        {
            price.text = InfValOperations.DisplayInfVal(currentWorkplaceController.WorkerProperties.
                CalculateCostOfNextLevel(levelsIncrementedByNumber));
            
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


        private void TurnOnManagerSelection()
        {
            SelectManagerPopupData data = new SelectManagerPopupData(currentWorkplaceController.GetWorkerManagers(), 
                DrawNewRandomManagers);
            MessageDispatcher.Instance.Send(new DisplaySelectManagerCommand(data));
        }


        private void InitializeManager(WorkerManagerStatistics workerManagerStatistics)
        {
            Debug.Log("InitializeManager");
            workerManagerFaceController.Initialize(workerManagerStatistics);
        }
        
        
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
                case NewWorkerManagerSelectedNotification newWorkerManagerSelectedNotification:
                {
                    InitializeManager(newWorkerManagerSelectedNotification.WorkerManagerStatistics);
                    currentWorkplaceController.SetWorkerManagerStatistics(newWorkerManagerSelectedNotification.WorkerManagerStatistics);
                    break;
                }
                
            }
        }
    }

    
}
