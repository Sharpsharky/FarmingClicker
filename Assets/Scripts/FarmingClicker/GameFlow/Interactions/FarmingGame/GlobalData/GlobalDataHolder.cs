using System;
using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.GlobalData
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class GlobalDataHolder : SerializedMonoBehaviour
    {

        [SerializeField, BoxGroup("Workers")] private List<StatisticType> statisticTypes = new List<StatisticType>();
        
        [SerializeField, BoxGroup("Managers")] public List<Color> colorsOfManagersIcons = new List<Color>();
        [SerializeField, BoxGroup("Managers")] public List<Sprite> spritesForManagersIconBackgrounds = new List<Sprite>();        
        [SerializeField, BoxGroup("Managers")] public List<Sprite> spritesForManagersFaces = new List<Sprite>();        
        
        public static GlobalDataHolder instance;
        
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Too many Instances of GlobalDataHolder");
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public StatisticType GetStatisticType(StatisticsTypes statisticsTypeToFind)
        {
            foreach (var statisticsType in statisticTypes)
            {
                if (statisticsTypeToFind == statisticsType.StatisticsType) return statisticsType;
            }
            return null;
        }
        
        
    }
}