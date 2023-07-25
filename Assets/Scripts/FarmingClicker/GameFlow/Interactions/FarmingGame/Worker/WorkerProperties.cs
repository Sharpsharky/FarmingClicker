using System.Collections.Generic;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    using InfiniteValue;
    using Data;

    [System.Serializable]
    public class WorkerProperties
    {
        protected int upgradeLevel = 0;
        protected int numberOfWorkers = 0;
        protected InfVal maxTransportedCurrency = new InfVal(0).ToPrecision(9);
        protected float workingSpeed = 0;
        protected float movingSpeed = 0;
        protected InfVal costOfNextLevel = new InfVal(0).ToPrecision(9);
        protected InfVal croppedCurrency = new InfVal(0).ToPrecision(9);

        protected InitialWorkerProperties initialWorkerProperties;

        #region Getters

        public int UpgradeLevel => upgradeLevel;
        public int NumberOfWorkers => numberOfWorkers;
        public InfVal MaxTransportedCurrency => maxTransportedCurrency;
        public float WorkingSpeed => workingSpeed;
        public float MovingSpeed => movingSpeed;
        public InfVal CostOfNextLevel => costOfNextLevel;
        public InfVal CroppedCurrency => croppedCurrency;

    
        #endregion
        
        public void SetInitialProperties(InitialWorkerProperties initialWorkerProperties)
        {
            this.initialWorkerProperties = initialWorkerProperties;
        }
        
        public void ChangeUpgradeLevel(int levelsToAdd)
        {
            numberOfWorkers = CalculateNumberOfWorkers(levelsToAdd);
            workingSpeed = CalculateWorkingSpeed(levelsToAdd);
            movingSpeed = CalculateMovingSpeed(levelsToAdd);
            costOfNextLevel = CalculateCostOfNextLevel(levelsToAdd);
            croppedCurrency = CalculateCroppedCurrency(levelsToAdd);
            maxTransportedCurrency = CalculateMaxTransportedCurrency(levelsToAdd);
            
            upgradeLevel = CalculateUpgradeLevel(levelsToAdd);

        }
        
        //"i" in the following getters increment the return values by the given number of levels
        
        public int CalculateUpgradeLevel(int i = 0)
        {
            return upgradeLevel + i;
        }
        public int CalculateNumberOfWorkers(int i = 0)
        {
            return initialWorkerProperties.NumberOfWorkers;
        }
        public virtual InfVal CalculateMaxTransportedCurrency(int i = 0)
        {
            InfVal cumulativeValue = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(2.51).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);   

            cumulativeValue = (initialWorkerProperties.MaxTransportedCurrency * pow)/ scalingValI;

            return cumulativeValue;
            
        }
        
        public float CalculateWorkingSpeed(int i = 0)
        {
            return 1 + (upgradeLevel+i) * 0.1f;
        }

        public float CalculateMovingSpeed(int i = 0)
        {
            return 1 + (upgradeLevel+i) * 0.02f;
        }

        public virtual InfVal CalculateCostOfNextLevel(int i = 0)
        {
            InfVal finalCost = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(2.17).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);   

            finalCost = (initialWorkerProperties.CostOfNextLevel * pow)/ scalingValI;

            return finalCost;
        }
        
        public virtual InfVal CalculateCroppedCurrency(int i = 0)
        {
            InfVal cumulativeValue = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

            int targetLevel = upgradeLevel + i;
            InfVal scalingValI = new InfVal(2.15).ToPrecision(InGameData.InfValPrecision);
            InfVal pow = MathInfVal.Pow(scalingValI, targetLevel + 1).ToPrecision(InGameData.InfValPrecision);   

            cumulativeValue = (initialWorkerProperties.CroppedCurrency * pow)/ scalingValI;

            return cumulativeValue;
        }

        public string GetValueOfStatistic(StatisticsTypes statisticsType, int levelIncrementation = 0)
        {
            switch (statisticsType)
            {
                case StatisticsTypes.LEVEL:
                {
                    return CalculateUpgradeLevel(levelIncrementation).ToString();
                }
                case StatisticsTypes.MAX_LOAD:
                {
                    return InfValOperations.DisplayInfVal(CalculateMaxTransportedCurrency(levelIncrementation));
                }
                case StatisticsTypes.MOVING_SPEED:
                {
                    return CalculateMovingSpeed(levelIncrementation).ToString();
                }
                case StatisticsTypes.WORKING_SPEED:
                {
                    return CalculateWorkingSpeed(levelIncrementation).ToString();
                }
                case StatisticsTypes.CROPPED_CURRENCY:
                {
                    return InfValOperations.DisplayInfVal(CalculateCroppedCurrency(levelIncrementation));
                }
                case StatisticsTypes.NUMBER_OF_WORKERS:
                {
                    return CalculateNumberOfWorkers(levelIncrementation).ToString();
                }
            }

            return "ERROR";
        }
        
    }
}