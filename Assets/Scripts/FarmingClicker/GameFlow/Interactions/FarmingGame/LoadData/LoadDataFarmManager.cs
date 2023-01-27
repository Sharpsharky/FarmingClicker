using System;
using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager.Data;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData
{
    public class LoadDataFarmManager : MonoBehaviour
    {
        public List<FarmFieldData> FarmFieldDatas = new List<FarmFieldData>();
        public FarmFieldCurrentlyBuildingData FarmFieldCurrentlyBuildingData = new FarmFieldCurrentlyBuildingData();
        public FarmGranaryData FarmGranaryData = new FarmGranaryData();
        public FarmShopData FarmShopData = new FarmShopData();
        public FarmCurrencyData FarmCurrencyData = new FarmCurrencyData();

        private const string FARM_FIELD_DATAS_SAVING_NAME = "farmFieldDatas";
        private const string FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME = "farmFieldCurrentlyBuildingData";
        private const string FARM_GRANARY_DATA_SAVING_NAME = "farmGranaryData";
        private const string FARM_SHOP_DATA_SAVING_NAME = "farmShopData";
        private const string FARM_CURRENCY_DATA_SAVING_NAME = "farmCurrencyData";

        private int currentNumberOfFarm = 0;

        public static LoadDataFarmManager instance;
        
        
        private void InitializeSingleton()
        {
            if (instance != null) instance = this;
            else
            {
                Debug.LogError("Too many instances!");
                Destroy(gameObject);
            }
        }

        public void Initialize(int numberOfFarm)
        {
            InitializeSingleton();
            
            currentNumberOfFarm = numberOfFarm;
            
            
            if(ES3.KeyExists(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm))) 
                FarmFieldDatas = ES3.Load<List<FarmFieldData>>(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm));

            if(ES3.KeyExists(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmFieldCurrentlyBuildingData = ES3.Load<FarmFieldCurrentlyBuildingData>(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm));
            
            if(ES3.KeyExists(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmGranaryData = ES3.Load<FarmGranaryData>(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm));
            
            if(ES3.KeyExists(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmShopData = ES3.Load<FarmShopData>(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm));

            if(ES3.KeyExists(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmCurrencyData = ES3.Load<FarmCurrencyData>(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm));
        }

        public void QuickSave()
        {
            SaveData();
        }

        private void SaveData()
        {
            ES3.Save(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm), FarmFieldDatas);
            ES3.Save(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm), FarmFieldCurrentlyBuildingData);
            ES3.Save(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm), FarmGranaryData);
            ES3.Save(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm), FarmShopData);
            ES3.Save(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm), FarmCurrencyData);
        }
        
        private string GetProperSavingName(string savingName, int numberOfFarm)
        {
            return savingName + numberOfFarm.ToString();
        }
        
    }
}
