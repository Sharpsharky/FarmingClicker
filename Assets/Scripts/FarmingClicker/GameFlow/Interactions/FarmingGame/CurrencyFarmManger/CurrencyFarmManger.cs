using System;
using System.Collections.Generic;
using InfiniteValue;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.CurrencyFarmManger
{
    public class CurrencyFarmManger : MonoBehaviour
    {
        public static List<InfVal> currentStorageOfEveryFarm { get; set; }
        public static InfVal currentStorageOfGranary { get; set; }


        public void Initialize(Currencies currencies)
        {
            currentStorageOfEveryFarm = currencies.CurrentStorageOfEveryFarm;
            currentStorageOfGranary = currencies.CurrentStorageOfGranary;
        }

        private void Awake()
        {
            currentStorageOfGranary = 0;
        }
    }
}
