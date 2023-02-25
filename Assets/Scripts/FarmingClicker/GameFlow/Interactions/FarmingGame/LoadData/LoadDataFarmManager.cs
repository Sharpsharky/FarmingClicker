namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData
{
    using System.Collections.Generic;
    using Data;
    using UnityEngine;
    
    public class LoadDataFarmManager : MonoBehaviour
    {
        public List<FarmFieldData> FarmFieldDatas = new List<FarmFieldData>();
        public List<FarmGranaryData> FarmGranaryData = new List<FarmGranaryData>();
        public List<FarmShopData> FarmShopData = new List<FarmShopData>();
        public FarmCurrencyData FarmCurrencyData;
        public FarmFieldCurrentlyBuildingData FarmFieldCurrentlyBuildingData;

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
                FarmGranaryData = ES3.Load<List<FarmGranaryData>>(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                AddEmptyGranaryData();
            }
            if(ES3.KeyExists(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmShopData = ES3.Load<List<FarmShopData>>(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                AddEmptyShopData();
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
        
        public void AddEmptyGranaryData()
        {
            var emptyGranaryData = new FarmGranaryData(0,1,"0");
            FarmGranaryData.Add(emptyGranaryData);
        }
        
        public void AddEmptyShopData()
        {
            var emptyShopData = new FarmShopData(0,1,"0");
            FarmShopData.Add(emptyShopData);
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
