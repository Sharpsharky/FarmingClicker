using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.CurrencyPerSecond
{
    using System.Collections.Generic;
    using Data;
    using Workplaces.FarmFields;
    using Workplaces.FarmShop;
    using Workplaces.Granary;
    using InfiniteValue;
    using LoadData;
    using Sirenix.OdinInspector;
    using System;
    using Core.Message;
    using Core.Message.Interfaces;
    using Messages.Commands.Currency;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;
    public class CurrencyPerSecondManager : SerializedMonoBehaviour, IMessageReceiver
    {
        private static float maxCurrencyOffset = 4;
        private static float maxTransportedOffset = 4;
        
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();
        private List<GranaryController> farmGranaryControllers = new List<GranaryController>();
        private List<FarmShopController> farmShopControllers = new List<FarmShopController>();

        public List<Type> ListenedTypes { get; } = new List<Type>();

        public void Initialize(List<FarmFieldController> farmFieldControllers, List<GranaryController> farmGranaryControllers, List<FarmShopController> farmShopControllers)
        {
            ListenedTypes.Add(typeof(ChangeStatisticsOfUpgradeNotification));
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
            this.farmFieldControllers = farmFieldControllers;
            this.farmGranaryControllers = farmGranaryControllers;
            this.farmShopControllers = farmShopControllers;
            CalculateCurrencyPerSecond();
            SendCommandToModifyCurrentCurrencyPerSec();
        }
        
        public InfVal CalculateCurrencyPerSecond()
        {
            var biggestCurrencyOfAllFarms = FindTheBiggestCurrencyOfAllFarms(farmFieldControllers);

            var maxTransportedCurrency = GetMinTransportedCurrency(farmGranaryControllers[0], farmShopControllers[0]);

            if (biggestCurrencyOfAllFarms > maxTransportedCurrency * maxCurrencyOffset) //TODO: Has to be tweaked
                maxTransportedCurrency = maxTransportedCurrency * maxCurrencyOffset; 
            
            if (maxTransportedCurrency > biggestCurrencyOfAllFarms * maxTransportedOffset) //TODO: Has to be tweaked
                biggestCurrencyOfAllFarms = biggestCurrencyOfAllFarms * maxTransportedOffset;
            
            
            InfVal currencyPerSecond = maxTransportedCurrency / biggestCurrencyOfAllFarms;
            
            return currencyPerSecond;
        }

        private InfVal FindTheBiggestCurrencyOfAllFarms(List<FarmFieldController> farmFieldControllers)
        {
            InfVal maxVal = new InfVal(0).ToPrecision(InGameData.InfValPrecision);
            foreach (var farmFieldController in farmFieldControllers)
            {

                maxVal += farmFieldController.WorkerProperties.CroppedCurrency *
                          farmFieldController.WorkerProperties.NumberOfWorkers;
                /*if (farmFieldController.WorkerProperties.CroppedCurrency * farmFieldController.WorkerProperties.
                    NumberOfWorkers > maxVal)
                    maxVal = farmFieldController.WorkerProperties.CroppedCurrency;*/
            }

            return maxVal;
        }

        private InfVal GetMinTransportedCurrency(GranaryController 
            granaryController, FarmShopController farmShopController)
        {
            
            var maxTransportedCurrencyOfGranary = granaryController.WorkerProperties.MaxTransportedCurrency *
                                                  granaryController.WorkerProperties.NumberOfWorkers;
            var maxTransportedCurrencyOfFarmShop = farmShopController.WorkerProperties.MaxTransportedCurrency *
                                                   farmShopController.WorkerProperties.NumberOfWorkers;
            
            if (maxTransportedCurrencyOfFarmShop < maxTransportedCurrencyOfGranary)
                return maxTransportedCurrencyOfFarmShop;
            else
                return maxTransportedCurrencyOfGranary;
        }

        private void SendCommandToModifyCurrentCurrencyPerSec()
        {
            var newCurrencyPerSecond = CalculateCurrencyPerSecond();
            Debug.Log("newCurrencyPerSecond: " + newCurrencyPerSecond);
            MessageDispatcher.Instance.Send(new SetCurrentCurrencyPerSecCommand(newCurrencyPerSecond));
        }
        
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch (message)
            {
                case ChangeStatisticsOfUpgradeNotification changeStatisticsOfUpgradeNotification:
                {
                    SendCommandToModifyCurrentCurrencyPerSec();
                    break;
                }
                case FarmFieldConstructedNotification farmFieldConstructedNotification:
                {
                    SendCommandToModifyCurrentCurrencyPerSec();
                    break;
                }
            }      
        }
    }
}