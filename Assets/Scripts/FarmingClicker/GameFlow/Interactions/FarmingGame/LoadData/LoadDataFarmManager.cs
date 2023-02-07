namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData
{
    using System.Collections.Generic;
    using Data;
    using UnityEngine;
    
    public class LoadDataFarmManager : MonoBehaviour
    {
        public List<FarmFieldData> FarmFieldDatas = new List<FarmFieldData>();
        public FarmFieldCurrentlyBuildingData FarmFieldCurrentlyBuildingData;
        public FarmGranaryData FarmGranaryData;
        public FarmShopData FarmShopData;
        public FarmCurrencyData FarmCurrencyData;

        private const string FARM_FIELD_DATAS_SAVING_NAME = "farmFieldDatas";
        private const string FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME = "farmFieldCurrentlyBuildingData";
        private const string FARM_GRANARY_DATA_SAVING_NAME = "farmGranaryData";
        private const string FARM_SHOP_DATA_SAVING_NAME = "farmShopData";
        private const string FARM_CURRENCY_DATA_SAVING_NAME = "farmCurrencyData";

        private int currentNumberOfFarm = 0;

        public static LoadDataFarmManager instance;
        
        
        private void InitializeSingleton()
        {
            if (instance == null) instance = this;
            else
            {
                Debug.LogError("Too many instances!");
                Destroy(gameObject);
            }
        }

        public void Initialize(int numberOfFarm)
        {
            Debug.Log("Initialize LoadDataFarmManager");
            InitializeSingleton();
            
            currentNumberOfFarm = numberOfFarm;
            
            
            if(ES3.KeyExists(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm))) 
                FarmFieldDatas = ES3.Load<List<FarmFieldData>>(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm));
            else
            {
                AddEmptyFarmField();
            }
            if(ES3.KeyExists(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmFieldCurrentlyBuildingData = ES3.Load<FarmFieldCurrentlyBuildingData>(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                FarmFieldCurrentlyBuildingData = new FarmFieldCurrentlyBuildingData("0");
            }
            if(ES3.KeyExists(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmGranaryData = ES3.Load<FarmGranaryData>(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                FarmGranaryData = new FarmGranaryData(0,1,"0");
            }
            if(ES3.KeyExists(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmShopData = ES3.Load<FarmShopData>(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                FarmShopData = new FarmShopData(0,1,"0");
            }
            if(ES3.KeyExists(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmCurrencyData = ES3.Load<FarmCurrencyData>(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                FarmCurrencyData = new FarmCurrencyData("0","0");
            }
        }

        public void AddEmptyFarmField()
        {
            var farmFieldData = new FarmFieldData(0,1,"0");
            FarmFieldDatas.Add(farmFieldData);
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
