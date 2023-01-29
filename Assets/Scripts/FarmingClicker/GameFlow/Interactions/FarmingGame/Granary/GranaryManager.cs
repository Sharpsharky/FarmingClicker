
using System;
using System.Collections.Generic;
using Core.Message.Interfaces;
using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Granary
{
    using UnityEngine;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager.Data;

    public class GranaryManager : MonoBehaviour
    {
        [SerializeField] private GameObject tractorPrefab;
        
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        
        private FarmData initialFarmData;
        
        public void Initialize(FarmData initialFarmData)
        {
            this.initialFarmData = initialFarmData;
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
                Instantiate(tractorPrefab, initialFarmData.StartingPoint, Quaternion.identity);
            }
            
        }
        
        
    }
}
