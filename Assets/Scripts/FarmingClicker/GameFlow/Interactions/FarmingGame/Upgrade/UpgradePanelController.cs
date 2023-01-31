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
        [SerializeField, BoxGroup("Buttons")] private Button upgrade1XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade5XButton;
        [SerializeField, BoxGroup("Buttons")] private Button upgrade10XButton;
        
        [SerializeField, BoxGroup("Statistics")] private TMP_Text currentValue;

        
        private int farmIndex = -1;

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

            farmIndex = upgradeDisplayPopupData.FarmIndex;
            
            
            title.text = upgradeDisplayPopupData.Title;
            currentValue.text = upgradeDisplayPopupData.CurrentVal.ToString();
            
            exitButton.onClick.AddListener(CloseGame);
            
            upgrade1XButton.onClick.AddListener(() =>{BuyUpgrade(1, upgradeDisplayPopupData.FarmWorkerType);});
            upgrade5XButton.onClick.AddListener(() =>{BuyUpgrade(5, upgradeDisplayPopupData.FarmWorkerType);});
            upgrade10XButton.onClick.AddListener(() =>{BuyUpgrade(10, upgradeDisplayPopupData.FarmWorkerType);});
            
            gameObject.SetActive(true);
        }

        private void BuyUpgrade(int amount, FarmWorkerType farmWorkerType)
        {
            if (farmWorkerType == FarmWorkerType.FARM_FIELD)
            {
                MessageDispatcher.Instance.Send(new BuyFarmFieldUpgradeCommand(farmIndex, amount));
            }
            else if (farmWorkerType == FarmWorkerType.TRACTOR)
            {
                MessageDispatcher.Instance.Send(new BuyGranaryUpgradeCommand(amount));
            }
            else
            {
                MessageDispatcher.Instance.Send(new BuyShoppingUpgradeCommand(amount));
            }

        }


        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case ChangeStatisticsOfUpgradeNotification changeStatisticsOfUpgradeNotification:
                {
                    currentValue.text = changeStatisticsOfUpgradeNotification.CurrentValueOfCroppedCurrency.ToString();
                    break;
                }
                
            }
        }
    }
}
