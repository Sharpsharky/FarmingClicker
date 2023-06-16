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
    using Data;

    public class FutureFarmFieldManager : MonoBehaviour, IMessageReceiver
    {
        private FarmCalculationData initialFarmCalculationData;
        private FutureFarmFieldController futureFarmFieldController;

        private int currentNumberOfFarms;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData, FutureFarmFieldController futureFarmFieldController)
        {
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.futureFarmFieldController = futureFarmFieldController;


            currentNumberOfFarms = LoadDataFarmManager.instance.FarmFieldControllers.Count;
            Debug.Log("FutureFarmFieldManager Initialize");
            InitializeFutureFarmFieldData();
            futureFarmFieldController.Initialize(initialFarmCalculationData);

            ListenedTypes.Add(typeof(BuyNewFieldCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            

        }

        private void InitializeFutureFarmFieldData()
        {
            currentNumberOfFarms = LoadDataFarmManager.instance.FarmFieldControllers.Count;

            var priceOfFutureFarmField = CalculatePriceOfFutureFarmField(currentNumberOfFarms).
                ToPrecision(InGameData.InfValPrecision);
            int timeOfConstruction = CalculateTimeOfConstruction(currentNumberOfFarms);
            
            futureFarmFieldController.SetDataForFutureFarmField(priceOfFutureFarmField, timeOfConstruction);
        }
        
        private InfVal CalculatePriceOfFutureFarmField(int numberOfOwnedFarmFields)
        {
            //Debug.Log($"numberOfOwnedFarmFields: { numberOfOwnedFarmFields}");
            //return 20 * numberOfOwnedFarmFields;
            
            InfVal finalPrice = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = numberOfOwnedFarmFields - 1;
            InfVal scalingValI = new InfVal(3.29f).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1, false).ToPrecision(InGameData.InfValPrecision);

            InfVal a = MathInfVal.Pow(60, numberOfOwnedFarmFields - 1, false).ToPrecision(InGameData.InfValPrecision);
            InfVal b = MathInfVal.Pow(7, numberOfOwnedFarmFields - 1, false).ToPrecision(InGameData.InfValPrecision);
            
            InfVal initialCroppedCurrency =
                new InfVal(a /b).ToPrecision(InGameData.InfValPrecision);
            
            finalPrice = (20 * initialCroppedCurrency * pow)/ scalingValI;
            
            return finalPrice;
            
        }
        
        private int CalculateTimeOfConstruction(int numberOfOwnedFarmFields)
        {
            return 1 * numberOfOwnedFarmFields;
        }


        private void BuyNewField()
        {
            currentNumberOfFarms++;
            futureFarmFieldController.PutFutureFarmInNewPosition();
            InitializeFutureFarmFieldData();
            
        }
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case BuyNewFieldCommand buyNewFieldCommand:
                {
                    BuyNewField();
                    break;
                }
                
            }
            
        }
    }
}
