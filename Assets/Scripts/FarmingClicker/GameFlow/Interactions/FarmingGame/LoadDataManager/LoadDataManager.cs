using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager.Data;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager
{
    public class LoadDataManager : MonoBehaviour
    {
        public static List<FarmFieldData> farmFieldDatas = new List<FarmFieldData>();
        public static FarmFieldCurrentlyBuildingData farmFieldCurrentlyBuildingData;
        public static FarmGranaryData farmPathData;
        public static FarmShopData farmShopData;
        public static FarmCurrencyData farmCurrencyData;
        

        public void Initialize()
        {
            
        }
        
        
    }
}
