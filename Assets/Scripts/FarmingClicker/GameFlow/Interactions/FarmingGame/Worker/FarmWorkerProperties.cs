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
            InfVal scalingValI = new InfVal(2.15).ToPrecision(InGameData.InfValPrecision) +100 * (numberOfFarm-1);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);
            InfVal initialCroppedCurrency =
                new InfVal(initialWorkerProperties.CroppedCurrency + 100 * (numberOfFarm - 1)).ToPrecision(InGameData.InfValPrecision);
            
            cumulativeValue = (initialCroppedCurrency * pow)/ scalingValI;

            return cumulativeValue;
        }
        public override InfVal CalculateCostOfNextLevel(int i = 0)
        {
            InfVal finalCost = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(2.17).ToPrecision(InGameData.InfValPrecision)+100 * (numberOfFarm-1);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);   

            InfVal initialCroppedCurrency =
                new InfVal(initialWorkerProperties.CostOfNextLevel + 100 * (numberOfFarm - 1)).ToPrecision(InGameData.InfValPrecision);
            
            finalCost = (initialCroppedCurrency * pow)/ scalingValI;
            
            return finalCost;
        }
        
        public override InfVal CalculateMaxTransportedCurrency(int i = 0)
        {
            InfVal cumulativeValue = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(2.51).ToPrecision(InGameData.InfValPrecision)+100 * (numberOfFarm-1);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);   

            InfVal initialCroppedCurrency =
                new InfVal(initialWorkerProperties.MaxTransportedCurrency + 100 * (numberOfFarm - 1)).ToPrecision(InGameData.InfValPrecision);
            
            cumulativeValue = (initialCroppedCurrency * pow)/ scalingValI;
            
            return cumulativeValue;
            
        }
        
    }
}