﻿namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using UnityEngine;

    public class FarmData
    {
        public Vector3 StartingPoint;
        public float YOfFirstStop;
        public float DistanceBetweenStops;
        public int NumberOfStops;
        public float YOfGarage;
        
        public FarmData(Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage)
        {
            StartingPoint = startingPoint;
            YOfFirstStop = yOfFirstStop;
            DistanceBetweenStops = distanceBetweenStops;
            NumberOfStops = numberOfStops;
            YOfGarage = yOfGarage;
        } 
    }
}