using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Granary
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmsSpawnerManager;
    using LoadData;
    using Messages.Commands.Upgrades;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager.Data;
    using InfiniteValue;
    
    public class GranaryManager : MonoBehaviour, IMessageReceiver
    {
        [SerializeField] private GameObject tractorPrefab;
        
        private FarmCalculationData initialFarmCalculationData;
        
        private FarmGranaryData granaryData;
        private GranaryController granaryController;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData, GranaryController granaryController)
        {
            this.granaryController = granaryController;
            this.initialFarmCalculationData = initialFarmCalculationData;
            
            ListenedTypes.Add(typeof(BuyGranaryUpgradeCommand));

            MessageDispatcher.Instance.RegisterReceiver(this);
            
            GetInitialData();
            InitializeWorkers();
            
        }

        private void GetInitialData()
        {
            int upgradeLevel = LoadDataFarmManager.instance.FarmGranaryData.upgradeLevel;
            int numberOfWorkers= LoadDataFarmManager.instance.FarmGranaryData.numberOfWorkers;
            InfVal currentCurrency = InfVal.Parse(LoadDataFarmManager.instance.FarmGranaryData.currentCurrency);
            InfVal currentValueOfCroppedCurrency = CalculateValueOfCroppedCurrency(upgradeLevel);
            
            granaryController.Initialize(upgradeLevel, numberOfWorkers, currentCurrency, currentValueOfCroppedCurrency);
        }

        private void InitializeWorkers()
        {
            for (int i = 0; i < granaryData.numberOfWorkers; i++)
            {
                Instantiate(tractorPrefab, initialFarmCalculationData.StartingPoint, Quaternion.identity);
            }
            
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
                case BuyGranaryUpgradeCommand buyFarmFieldUpgradeCommand:
                {
                    granaryData.upgradeLevel +=
                        buyFarmFieldUpgradeCommand.AmountOfBoughtUpgrades;

                    var currentValueOfCroppedCurrency = granaryController.SetValueOfTransportedCurrency(
                        CalculateValueOfCroppedCurrency(granaryData.upgradeLevel));
                    
                    MessageDispatcher.Instance.Send(new ChangeStatisticsOfUpgradeNotification(currentValueOfCroppedCurrency));
                    
                    break;
                }
                
            }
        }
        
        
    }
}
