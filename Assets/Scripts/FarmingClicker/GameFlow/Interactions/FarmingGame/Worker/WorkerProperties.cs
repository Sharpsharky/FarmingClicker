﻿namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
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
        public InfVal CalculateMaxTransportedCurrency(int i = 0)
        {
            return initialWorkerProperties.MaxTransportedCurrency +(10 * (upgradeLevel + i));
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
            /*float a = 3.67f;
            InfVal b = Mathf.Pow(1.07f, upgradeLevel);
            InfVal c = Mathf.Pow(1.07f, i)-1;
            float d = 1.07f - 1;

            InfVal finalCost = a * ((b * c) / d);
*/
            InfVal finalCost = 0;

            int targetLevel = upgradeLevel + i;
            float scalingVal = 2.17f;
            finalCost = (initialWorkerProperties.CroppedCurrency * Mathf.Pow(scalingVal,targetLevel+1))/ scalingVal;
            return finalCost;
            
            
            
            return finalCost;
        }
        
        public InfVal CalculateCroppedCurrency(int i = 0)
        {
            InfVal cumulativeValue = 0;

            int targetLevel = upgradeLevel + i;
            float scalingVal = 2.15f;
            cumulativeValue = (initialWorkerProperties.CroppedCurrency * Mathf.Pow(scalingVal,targetLevel+1))/ scalingVal;
            return cumulativeValue;
        }
        
    }
}