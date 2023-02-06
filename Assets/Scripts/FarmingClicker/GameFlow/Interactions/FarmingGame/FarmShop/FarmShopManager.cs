using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Messages.Commands.Upgrades;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;
    using InfiniteValue;
    using UnityEngine;
    
    public class FarmShopManager : MonoBehaviour, IMessageReceiver
    {
        private FarmShopData farmShopData;
        private FarmShopController farmShopController;
        private FarmCalculationData initialFarmCalculationData;

        public void Initialize(FarmCalculationData initialFarmCalculationData, FarmShopController farmShopController)
        {
            this.farmShopController = farmShopController;
            this.initialFarmCalculationData = initialFarmCalculationData;
            
            ListenedTypes.Add(typeof(BuyShoppingUpgradeCommand));
            
            MessageDispatcher.Instance.RegisterReceiver(this);

            GetInitialData();

        }
        
        private void GetInitialData()
        {
            int upgradeLevel = LoadDataFarmManager.instance.FarmGranaryData.upgradeLevel;
            int numberOfWorkers = LoadDataFarmManager.instance.FarmGranaryData.numberOfWorkers;
            InfVal currentCurrency = InfVal.Parse(LoadDataFarmManager.instance.FarmGranaryData.currentCurrency);
            InfVal currentValueOfCroppedCurrency = CalculateValueOfCroppedCurrency(upgradeLevel);
            
            farmShopController.Initialize(upgradeLevel, numberOfWorkers, currentCurrency, currentValueOfCroppedCurrency);
            
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
