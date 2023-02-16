using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor;
using FarmingClicker.GameFlow.Messages.Commands.NewField;
using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;

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
    using InfiniteValue;
    
    public class GranaryManager : MonoBehaviour, IMessageReceiver
    {
        [SerializeField] private GameObject tractorPrefab;
        [SerializeField] private List<TractorController> tractorControllers = new List<TractorController>();
        private FarmCalculationData initialFarmCalculationData;
        
        private GranaryController granaryController;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData, GranaryController granaryController)
        {
            this.granaryController = granaryController;
            this.initialFarmCalculationData = initialFarmCalculationData;
            
            ListenedTypes.Add(typeof(BuyGranaryUpgradeCommand));
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));

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
            InfVal valX5 = CalculateValueOfCroppedCurrency(upgradeLevel+5);;
            InfVal valX10 = CalculateValueOfCroppedCurrency(upgradeLevel+10);;
            InfVal valX50 = CalculateValueOfCroppedCurrency(upgradeLevel+50);;
            
            granaryController.Initialize(upgradeLevel, numberOfWorkers, currentCurrency, 
                currentValueOfCroppedCurrency, valX5, valX10, valX50);
        }

        private void InitializeWorkers()
        {
            for (int i = 0; i < granaryController.NumberOfWorkers; i++)
            {
                GameObject newTractor = Instantiate(tractorPrefab, initialFarmCalculationData.StartingPoint, Quaternion.identity);
                var newTractorController = newTractor.GetComponent<TractorController>();
                tractorControllers.Add(newTractorController);

                newTractorController.Initialize(initialFarmCalculationData.FarmFieldControllers, 
                    initialFarmCalculationData.StartingPoint, initialFarmCalculationData.YOfFirstStop, 
                    initialFarmCalculationData.DistanceBetweenStops, initialFarmCalculationData.NumberOfStops, 
                    initialFarmCalculationData.YOfGarage, 
                    initialFarmCalculationData.GranaryController.gameObject.transform.position);
            }
            
        }
        
        private InfVal CalculateValueOfCroppedCurrency(int upgradeLevel)
        {
            return 1 * upgradeLevel;
        }

        private void AddNewFieldForTractors(FarmFieldController farmFieldController)
        {
            foreach (var tractor in tractorControllers)
            {
                tractor.AddNewField(farmFieldController);
            }
        }
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case BuyGranaryUpgradeCommand buyFarmFieldUpgradeCommand:
                {
                    granaryController.UpgradeLevel +=
                        buyFarmFieldUpgradeCommand.AmountOfBoughtUpgrades;

                    var currentValueOfCroppedCurrency = granaryController.SetValueOfTransportedCurrency(
                        CalculateValueOfCroppedCurrency(granaryController.UpgradeLevel));
                    
                    MessageDispatcher.Instance.Send(new ChangeStatisticsOfUpgradeNotification(currentValueOfCroppedCurrency));
                    
                    break;
                }
                case FarmFieldConstructedNotification buyNewFieldCommand:
                {
                    AddNewFieldForTractors(buyNewFieldCommand.NewFarmFieldController);
                    break;
                }
                
                
            }
        }
        
        
    }
}
