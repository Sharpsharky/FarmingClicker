namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.FutureFarmField
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmsSpawnerManager;
    using LoadData;
    using FarmingClicker.GameFlow.Messages.Commands.NewField;
    using InfiniteValue;
    using UnityEngine;
    public class FutureFarmFieldManager : MonoBehaviour, IMessageReceiver
    {
        private FarmCalculationData initialFarmCalculationData;
        private FutureFarmFieldController futureFarmFieldController;

        private int currentNumberOfFarms;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData, FutureFarmFieldController futureFarmFieldController)
        {
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.futureFarmFieldController = futureFarmFieldController;


            currentNumberOfFarms = LoadDataFarmManager.instance.FarmFieldDatas.Count;

            var priceOfFutureFarmField = CalculatePriceOfFutureFarmField(currentNumberOfFarms);
            int timeOfConstruction = CalculateTimeOfConstruction(currentNumberOfFarms);
            
            futureFarmFieldController.Initialize(initialFarmCalculationData, priceOfFutureFarmField, timeOfConstruction);
            
            ListenedTypes.Add(typeof(BuyNewFieldCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            

        }
        
        private InfVal CalculatePriceOfFutureFarmField(int numberOfOwnedFarmFields)
        {
            return 20 * numberOfOwnedFarmFields;
        }
        
        private int CalculateTimeOfConstruction(int numberOfOwnedFarmFields)
        {
            return 1 * numberOfOwnedFarmFields;
        }


        private void PutFutureFarmInNewPosition()
        {
            currentNumberOfFarms++;
            var posOfFutureFarm = futureFarmFieldController.gameObject.transform.position;
            posOfFutureFarm.y -= initialFarmCalculationData.DistanceBetweenStops;
            futureFarmFieldController.gameObject.transform.position = posOfFutureFarm;
        }
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case BuyNewFieldCommand buyNewFieldCommand:
                {
                    PutFutureFarmInNewPosition();
                    break;
                }
                
            }
            
        }
    }
}
