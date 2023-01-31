﻿using InfiniteValue;
using Sirenix.OdinInspector;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop
{
    public class FarmShopController : SerializedMonoBehaviour
    {
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfTransportedCurrency = 0;
        
        public void Initialize(int upgradeLevel, int numberOfWorkers, InfVal currentCurrency, InfVal valueOfTransportedCurrency)
        {
            this.upgradeLevel = upgradeLevel;
            this.numberOfWorkers = numberOfWorkers;
            this.currentCurrency = currentCurrency;
            SetValueOfTransportedCurrency(valueOfTransportedCurrency);
        }
        
        public InfVal SetValueOfTransportedCurrency(InfVal valueOfTransportedCurrency)
        {
            this.valueOfTransportedCurrency = valueOfTransportedCurrency;

            return valueOfTransportedCurrency;
        }
    }
}