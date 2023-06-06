namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    using InfiniteValue;
    using UnityEngine;

    public class WorkerProperties
    {
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal maxTransportedCurrency = 0;
        private float workingSpeed = 0;
        private float movingSpeed = 0;
        private InfVal costOfNextLevel = 0;
        private InfVal croppedCurrency = 0;
        
        private InitialWorkerProperties initialWorkerProperties;

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
            upgradeLevel = CalculateUpgradeLevel(levelsToAdd);
            numberOfWorkers = CalculateNumberOfWorkers(levelsToAdd);
            maxTransportedCurrency = CalculateMaxTransportedCurrency(levelsToAdd);
            workingSpeed = CalculateWorkingSpeed(levelsToAdd);
            movingSpeed = CalculateMovingSpeed(levelsToAdd);
            costOfNextLevel = CalculateCostOfNextLevel(levelsToAdd);
            croppedCurrency = CalculateCroppedCurrency(levelsToAdd);
        }
        
        //"i" in the following getters increment the return values by the given number of levels
        
        public int CalculateUpgradeLevel(int i = 0)
        {
            return upgradeLevel + i;
        }
        public int CalculateNumberOfWorkers(int i = 0)
        {
            return 1;
        }
        public InfVal CalculateMaxTransportedCurrency(int i = 0)
        {
            return 10 * (upgradeLevel+ 1 + i);
        }
        
        public float CalculateWorkingSpeed(int i = 0)
        {
            return 1 + (upgradeLevel+i) * 0.1f;
        }

        public float CalculateMovingSpeed(int i = 0)
        {
            return 1 + (upgradeLevel+i) * 0.02f;
        }

        public InfVal CalculateCostOfNextLevel(int i = 0)
        {
            float a = 3.67f;
            InfVal b = Mathf.Pow(1.07f, upgradeLevel);
            InfVal c = Mathf.Pow(1.07f, i)-1;
            float d = 1.07f - 1;

            InfVal finalCost = a * ((b * c) / d);

            return finalCost;
        }
        
        public InfVal CalculateCroppedCurrency(int i = 0)
        {
            InfVal collectedValue = 0;

            for (int j = 0; j < i + 1; j++)
            {
                collectedValue += 1.67f * (upgradeLevel+j+1) * 1;
            }
            
            return collectedValue;
        }
        
        
    }
}