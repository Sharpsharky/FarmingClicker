using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager.Data;
    using Messages.Commands.Upgrades;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;
    using InfiniteValue;
    using UnityEngine;
    
    public class FarmShopManager : MonoBehaviour, IMessageReceiver
    {
        private FarmShopData farmShopData;
        private FarmShopController farmShopController;
        
        public void Initialize(FarmShopController farmShopController)
        {
            this.farmShopController = farmShopController;
            
            ListenedTypes.Add(typeof(BuyShoppingUpgradeCommand));
            
            MessageDispatcher.Instance.RegisterReceiver(this);

            GetInitialData();

        }
        
        
        private void GetInitialData()
        {
            farmShopData.upgradeLevel = LoadDataFarmManager.instance.FarmGranaryData.upgradeLevel;
            farmShopData.numberOfWorkers = LoadDataFarmManager.instance.FarmGranaryData.numberOfWorkers;
        }
        
        
        private InfVal CalculateValueOfCroppedCurrency(int upgradeLevel)
        {
            return 1 * upgradeLevel;
        }
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case BuyShoppingUpgradeCommand buyFarmFieldUpgradeCommand:
                {
                    farmShopData.upgradeLevel +=
                        buyFarmFieldUpgradeCommand.AmountOfBoughtUpgrades;

                    var currentValueOfCroppedCurrency = farmShopController.SetValueOfTransportedCurrency(
                        CalculateValueOfCroppedCurrency(farmShopData.upgradeLevel));
                    
                    MessageDispatcher.Instance.Send(new ChangeStatisticsOfUpgradeNotification(currentValueOfCroppedCurrency));
                    
                    break;
                }
                
            }
        }
    }
}
