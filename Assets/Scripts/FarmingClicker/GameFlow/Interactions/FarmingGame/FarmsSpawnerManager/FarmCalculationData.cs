namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using UnityEngine;
    using System.Collections.Generic;
    using FarmFields;
    using FarmShop;
    using FutureFarmField;
    using Granary;
    
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
        public FutureFarmFieldController FutureFarmFieldController;
        public float XOfFirstUpgradeFarmFieldButton;
        
        
        public FarmCalculationData(Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, 
            List<FarmFieldController> farmFieldControllers, GranaryController granaryController,
            FarmShopController farmShopController, FutureFarmFieldController futureFarmFieldController, 
            float xOfFirstUpgradeFarmFieldButton)
        {
            StartingPoint = startingPoint;
            YOfFirstStop = yOfFirstStop;
            DistanceBetweenStops = distanceBetweenStops;
            NumberOfStops = numberOfStops;
            YOfGarage = yOfGarage;
            FarmFieldControllers = farmFieldControllers;
            GranaryController = granaryController;
            FarmShopController = farmShopController;
            FutureFarmFieldController = futureFarmFieldController;
            XOfFirstUpgradeFarmFieldButton = xOfFirstUpgradeFarmFieldButton;
        } 
    }
}