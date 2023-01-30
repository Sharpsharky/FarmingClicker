namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using UnityEngine;
    using System.Collections.Generic;
    using FarmFields;
    
    public class FarmData
    {
        public Vector3 StartingPoint;
        public float YOfFirstStop;
        public float DistanceBetweenStops;
        public int NumberOfStops;
        public float YOfGarage;
        public List<FarmFieldController> FarmFieldControllers;

        public FarmData(Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, 
            List<FarmFieldController> farmFieldControllers)
        {
            StartingPoint = startingPoint;
            YOfFirstStop = yOfFirstStop;
            DistanceBetweenStops = distanceBetweenStops;
            NumberOfStops = numberOfStops;
            YOfGarage = yOfGarage;
            FarmFieldControllers = farmFieldControllers;
        } 
    }
}