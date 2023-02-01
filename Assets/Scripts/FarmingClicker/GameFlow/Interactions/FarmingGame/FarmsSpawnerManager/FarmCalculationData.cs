using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Granary;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using UnityEngine;
    using System.Collections.Generic;
    using FarmFields;
    
    public class FarmCalculationData
    {
        public Vector3 StartingPoint;
        public float YOfFirstStop;
        public float DistanceBetweenStops;
        public int NumberOfStops;
        public float YOfGarage;
        public List<FarmFieldController> FarmFieldControllers;
        public GranaryController GranaryController;
        public FarmShopController FarmShopController;

        
        
        public FarmCalculationData(Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, 
            List<FarmFieldController> farmFieldControllers, GranaryController granaryController,
            FarmShopController farmShopController)
        {
            StartingPoint = startingPoint;
            YOfFirstStop = yOfFirstStop;
            DistanceBetweenStops = distanceBetweenStops;
            NumberOfStops = numberOfStops;
            YOfGarage = yOfGarage;
            FarmFieldControllers = farmFieldControllers;
            GranaryController = granaryController;
            FarmShopController = farmShopController;
        } 
    }
}