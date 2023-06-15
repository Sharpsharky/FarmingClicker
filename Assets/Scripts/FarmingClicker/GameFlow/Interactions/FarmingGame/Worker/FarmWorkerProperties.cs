using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    using Data;
    using InfiniteValue;

    [System.Serializable]
    public class FarmWorkerProperties : WorkerProperties
    {
        protected int numberOfFarm = 1;

        public void SetNumberOfFarm(int nr)
        {
            numberOfFarm = nr;
        }
        
        public override InfVal CalculateCroppedCurrency(int i = 0)
        {
            InfVal cumulativeValue = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(1.9f).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);

            InfVal a = MathInfVal.Pow(60, numberOfFarm - 1, false).ToPrecision(InGameData.InfValPrecision);
            InfVal b = MathInfVal.Pow(7, numberOfFarm - 1, false).ToPrecision(InGameData.InfValPrecision);
            
            InfVal initialCroppedCurrency =
                new InfVal(a /b).ToPrecision(InGameData.InfValPrecision);
            
            cumulativeValue = (initialWorkerProperties.CroppedCurrency * initialCroppedCurrency * pow)/ scalingValI;
            
            return cumulativeValue;
        }
        public override InfVal CalculateCostOfNextLevel(int i = 0)
        {
            InfVal finalCost = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(1.81f).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);

            InfVal a = MathInfVal.Pow(60, numberOfFarm - 1, false).ToPrecision(InGameData.InfValPrecision);
            InfVal b = MathInfVal.Pow(7, numberOfFarm - 1, false).ToPrecision(InGameData.InfValPrecision);
            
            InfVal initialCroppedCurrency =
                new InfVal(a /b).ToPrecision(InGameData.InfValPrecision);
            
            finalCost = (initialWorkerProperties.CostOfNextLevel * initialCroppedCurrency * pow)/ scalingValI;
            
            return finalCost;
        }
        
        public override InfVal CalculateMaxTransportedCurrency(int i = 0)
        {
            InfVal cumulativeValue = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(1.81f).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);

            InfVal a = MathInfVal.Pow(60, numberOfFarm - 1, false).ToPrecision(InGameData.InfValPrecision);
            InfVal b = MathInfVal.Pow(7, numberOfFarm - 1, false).ToPrecision(InGameData.InfValPrecision);
            
            InfVal initialCroppedCurrency =
                new InfVal(a /b).ToPrecision(InGameData.InfValPrecision);
            
            cumulativeValue = (initialWorkerProperties.MaxTransportedCurrency * initialCroppedCurrency * pow)/ scalingValI;
            
            return cumulativeValue;
            
        }
        
    }
}