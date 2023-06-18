namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData
{
    using System.Collections.Generic;
    using Data;
    using UnityEngine;
    using Workplaces.FarmShop;
    using Workplaces.Granary;
    using InfiniteValue;
    using Workplaces.FarmFields;
    using System.Collections;
    using FarmingClicker.Data;
    using CurrencyFarm;
    using Core.Message;
    using Messages.Commands.Currency;
    public class LoadDataFarmManager : MonoBehaviour
    {
        public List<WorkPlaceData> FarmFieldDatas = new List<WorkPlaceData>();
        public List<WorkPlaceData> FarmGranaryData = new List<WorkPlaceData>();
        public List<WorkPlaceData> FarmShopData = new List<WorkPlaceData>();
        public FarmCurrencyData FarmCurrencyData;
        public FarmFieldCurrentlyBuildingData FarmFieldCurrentlyBuildingData;

        public List<FarmFieldController> FarmFieldControllers = new List<FarmFieldController>();
        public List<GranaryController> FarmGranaryControllers = new List<GranaryController>();
        public List<FarmShopController> FarmShopControllers = new List<FarmShopController>();
        
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
                FarmFieldDatas = ES3.Load<List<WorkPlaceData>>(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm));
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
                FarmGranaryData = ES3.Load<List<WorkPlaceData>>(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                AddEmptyGranaryData();
            }
            if(ES3.KeyExists(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm))) 
                FarmShopData = ES3.Load<List<WorkPlaceData>>(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm));
            else
            {
                AddEmptyShopData();
            }

            if (ES3.KeyExists(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm)))
            {
                FarmCurrencyData =
                    ES3.Load<FarmCurrencyData>(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME,
                        currentNumberOfFarm));
                
                
                Debug.Log($"FarmCurrencyData: {FarmCurrencyData.currentCurrencyValue}, " +
                          $"{FarmCurrencyData.currentSuperCurrencyValue}, {FarmCurrencyData.currentValueOnSecond}");
                InfVal currentCurrencyValue = InfVal.Parse(FarmCurrencyData.currentCurrencyValue);
                InfVal currentSuperCurrencyValue = InfVal.Parse(FarmCurrencyData.currentSuperCurrencyValue);
                InfVal currentValueOnSecond = InfVal.Parse(FarmCurrencyData.currentValueOnSecond);
                
                MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(currentCurrencyValue));
                MessageDispatcher.Instance.Send(new ModifySuperCurrencyCommand(currentSuperCurrencyValue));

            }
            else
            {
                FarmCurrencyData = new FarmCurrencyData(0,0, 0);
            }

            StartCoroutine(SaveLoop());
        }

        public void AddEmptyFarmField()
        {
            var farmFieldData = new FarmFieldData(0,0);
            FarmFieldDatas.Add(farmFieldData);
        }
        
        public void AddEmptyGranaryData()
        {
            var emptyGranaryData = new FarmGranaryData(0,0);
            FarmGranaryData.Add(emptyGranaryData);
        }
        
        public void AddEmptyShopData()
        {
            var emptyShopData = new FarmShopData(0,0);
            FarmShopData.Add(emptyShopData);
        }
        
        private IEnumerator SaveLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                QuickSave();
            }

            yield return null;
        }
        
        public void QuickSave()
        {
            GetData();
            SaveData();
        }

        private void GetData()
        {
            FarmFieldDatas = new List<WorkPlaceData>();
            for (int i = 0; i < FarmFieldControllers.Count; i++)
            {
                FarmFieldDatas.Add(FarmFieldControllers[i].GetSavingData());
            }
            
            FarmGranaryData = new List<WorkPlaceData>();
            for (int i = 0; i < FarmGranaryControllers.Count; i++)
            {
                FarmGranaryData.Add(FarmGranaryControllers[i].GetSavingData());
            }
            
            FarmShopData = new List<WorkPlaceData>();
            for (int i = 0; i < FarmShopControllers.Count; i++)
            {
                FarmShopData.Add(FarmShopControllers[i].GetSavingData());
            }

            FarmCurrencyData = new FarmCurrencyData(CurrencyFarmManger.GetCurrentCurrency(), 
                CurrencyFarmManger.GetCurrentSuperCurrency(), 1);

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
