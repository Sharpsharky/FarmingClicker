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
        
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        
        private FarmCalculationData initialFarmCalculationData;
        
        private FarmGranaryData granaryData;
        private GranaryController granaryController;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData)
        {
            ListenedTypes.Add(typeof(BuyGranaryUpgradeCommand));

            MessageDispatcher.Instance.RegisterReceiver(this);

            
            this.initialFarmCalculationData = initialFarmCalculationData;
            GetInitialData();
            InitializeWorkers();
        }

        private void GetInitialData()
        {
            LoadDataFarmManager.instance.FarmGranaryData.upgradeLevel = upgradeLevel;
            LoadDataFarmManager.instance.FarmGranaryData.numberOfWorkers = numberOfWorkers;
        }

        private void InitializeWorkers()
        {
            for (int i = 0; i < numberOfWorkers; i++)
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
