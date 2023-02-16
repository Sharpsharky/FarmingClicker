using FarmingClicker.GameFlow.Interactions.FarmingGame.General;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Upgrade
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmingClicker.Data.Popup;
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Messages.Commands.Upgrades;
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
            
            valueStatistic.InitializeStatistic(levelStatisticComponents.GetIcon(),
                levelStatisticComponents.GetTitle(),upgradeDisplayPopupData.CurrentVal.ToString(), 
                upgradeDisplayPopupData.ValX5.ToString(), upgradeDisplayPopupData.ValX10.ToString());
            
            exitButton.onClick.AddListener(CloseGame);
            
            upgrade1XButton.onClick.AddListener(() =>{BuyUpgrade(1, upgradeDisplayPopupData.FarmWorker);});
            upgrade5XButton.onClick.AddListener(() =>{BuyUpgrade(5, upgradeDisplayPopupData.FarmWorker);});
            upgrade10XButton.onClick.AddListener(() =>{BuyUpgrade(10, upgradeDisplayPopupData.FarmWorker);});
            
            gameObject.SetActive(true);
        }

        private void BuyUpgrade(int amount, IFarmWorkerControllable farmWorkerType)
        {
            farmWorkerType.BuyUpgrade(amount);
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
