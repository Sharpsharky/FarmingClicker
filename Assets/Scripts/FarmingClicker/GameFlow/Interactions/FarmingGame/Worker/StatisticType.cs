using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    [Serializable]
    public class StatisticType
    {
        public string Name;
        public StatisticsTypes StatisticsType;
        public Sprite StatisticIcon;
    }
}