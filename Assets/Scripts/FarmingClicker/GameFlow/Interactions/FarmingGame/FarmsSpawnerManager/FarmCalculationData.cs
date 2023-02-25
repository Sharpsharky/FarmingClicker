using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using UnityEngine;
    using System.Collections.Generic;
    using FutureFarmField;
    using Workplaces.FarmFields;
    using Workplaces.FarmShop;
    using Workplaces.Granary;
    public class FarmCalculationData
    {
        public Vector3 StartingPoint;
        public float YOfFirstStop;
        public float DistanceBetweenStops;
        public int NumberOfStops;
        public float YOfGarage;
        public List<FarmFieldController> FarmFieldControllers;
        public List<GranaryController> GranaryControllers;
        public List<FarmShopController> FarmShopControllers;
        public FutureFarmFieldController FutureFarmFieldController;
        public float XOfFirstUpgradeFarmFieldButton;

        
        public FarmCalculationData(Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, 
            List<FarmFieldController> farmFieldControllers, List<GranaryController> granaryControllers,
            List<FarmShopController> farmShopControllers, FutureFarmFieldController futureFarmFieldController, 
            float xOfFirstUpgradeFarmFieldButton)
        {
            StartingPoint = startingPoint;
            YOfFirstStop = yOfFirstStop;
            DistanceBetweenStops = distanceBetweenStops;
            NumberOfStops = numberOfStops;
            YOfGarage = yOfGarage;
            FarmFieldControllers = new List<FarmFieldController>(farmFieldControllers);
            GranaryControllers = new List<GranaryController>(granaryControllers);
            FarmShopControllers = new List<FarmShopController>(farmShopControllers);
            FutureFarmFieldController = futureFarmFieldController;
            XOfFirstUpgradeFarmFieldButton = xOfFirstUpgradeFarmFieldButton;
        } 
    }
}