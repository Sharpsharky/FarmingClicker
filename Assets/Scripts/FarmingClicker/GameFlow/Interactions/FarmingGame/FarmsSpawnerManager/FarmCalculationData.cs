using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.FutureFarmField;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using UnityEngine;
    using System.Collections.Generic;
    using Workplaces.FarmFields;
    using Workplaces.FarmShop;
    using Workplaces.Granary;
    public class FarmCalculationData
    {
        public Vector3 StartingPoint;
        public float XOfUpgradeFarmButton;
        public float YOfFirstStop;
        public float DistanceBetweenStops;
        public int NumberOfStops;
        public float YOfGarage;
        public List<FarmFieldController> FarmFieldControllers;
        public List<GranaryController> GranaryControllers;
        public List<FarmShopController> FarmShopControllers;
        public FutureFarmFieldController FutureFarmFieldController;
        public float XOfFirstUpgradeFarmFieldButton;
        public Vector3 LeftEdgePosition;
        public Vector3 RightEdgePosition;
        
        public FarmCalculationData(Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, 
            List<FarmFieldController> farmFieldControllers, List<GranaryController> granaryControllers,
            List<FarmShopController> farmShopControllers, FutureFarmFieldController futureFarmFieldController, 
            float xOfFirstUpgradeFarmFieldButton, Vector3 leftEdgePosition, Vector3 rightEdgePosition)
        {
            StartingPoint = startingPoint;
            YOfFirstStop = yOfFirstStop;
            DistanceBetweenStops = distanceBetweenStops;
            NumberOfStops = numberOfStops;
            YOfGarage = yOfGarage;
            FarmFieldControllers = farmFieldControllers;
            GranaryControllers = new List<GranaryController>(granaryControllers);
            FarmShopControllers = new List<FarmShopController>(farmShopControllers);
            FutureFarmFieldController = futureFarmFieldController;
            XOfFirstUpgradeFarmFieldButton = xOfFirstUpgradeFarmFieldButton;
            LeftEdgePosition = leftEdgePosition;
            RightEdgePosition = rightEdgePosition;
        } 
    }
}