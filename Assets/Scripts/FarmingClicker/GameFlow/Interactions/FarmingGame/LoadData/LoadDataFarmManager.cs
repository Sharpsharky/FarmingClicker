using FarmingClicker.GameFlow.Messages.Commands.NewField;

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
    using CurrencyFarm;
    using Core.Message;
    using Messages.Commands.Currency;
    using System;
    using Core.Message.Interfaces;
    using Messages.Commands.Backend;

    public class LoadDataFarmManager : MonoBehaviour, IMessageReceiver
    {
        [SerializeField] private int maxProfitableDaysOffline = 30;
        
        public List<WorkPlaceData> FarmFieldDatas = new List<WorkPlaceData>();
        public List<WorkPlaceData> FarmGranaryData = new List<WorkPlaceData>();
        public List<WorkPlaceData> FarmShopData = new List<WorkPlaceData>();
        public FarmCurrencyData FarmCurrencyData;
        public FarmFieldCurrentlyBuildingData FarmFieldCurrentlyBuildingData;
        public PlayerFarmData PlayerFarmData;

        public List<FarmFieldController> FarmFieldControllers = new List<FarmFieldController>();
        public List<GranaryController> FarmGranaryControllers = new List<GranaryController>();
        public List<FarmShopController> FarmShopControllers = new List<FarmShopController>();
        public bool hasPlayedTheGameBefore = false;
        public DateTime LastTimePlayerOnline;
        
        private const string FARM_FIELD_DATAS_SAVING_NAME = "farmFieldDatas";
        private const string FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME = "farmFieldCurrentlyBuildingData";
        private const string FARM_GRANARY_DATA_SAVING_NAME = "farmGranaryData";
        private const string FARM_SHOP_DATA_SAVING_NAME = "farmShopData";
        private const string FARM_CURRENCY_DATA_SAVING_NAME = "farmCurrencyData";
        private const string FARM_PLAYER_DATA_SAVING_NAME = "farmPlayerData";
        private const string FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME = "offlineCurrencyData";

        public int SecondsOffline = 0;
        
        private int currentNumberOfFarm = 0;

        public static LoadDataFarmManager instance;
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
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
            ListenedTypes.Add(typeof(ResetBackendCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
            Debug.Log("Initialize LoadDataFarmManager");
            InitializeSingleton();
            
            currentNumberOfFarm = numberOfFarm;
            
            
            if(ES3.KeyExists(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm))) 
                FarmFieldDatas = ES3.Load<List<WorkPlaceData>>(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm));
            else
            {
                AddEmptyFarmField();
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

                InfVal currentCurrencyValue = InfVal.Parse(FarmCurrencyData.currentCurrencyValue);
                InfVal currentSuperCurrencyValue = InfVal.Parse(FarmCurrencyData.currentSuperCurrencyValue);

                InfVal offlineCurrencyValue = new InfVal(0);
                
                if (ES3.KeyExists(GetProperSavingName(FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm)))
                {
                    offlineCurrencyValue = InfVal.Parse(
                        ES3.Load<string>(GetProperSavingName(FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME,
                            currentNumberOfFarm)));
                    
                    currentCurrencyValue += offlineCurrencyValue;

                    SaveOfflineCurrency(0);
                }
                
                MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(currentCurrencyValue));
                MessageDispatcher.Instance.Send(new ModifySuperCurrencyCommand(currentSuperCurrencyValue));

            }
            else
            {
                FarmCurrencyData = new FarmCurrencyData(0,0);
            }

            if (ES3.KeyExists(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm)))
            {
                PlayerFarmData = ES3.Load<PlayerFarmData>(FARM_PLAYER_DATA_SAVING_NAME);

                LastTimePlayerOnline = PlayerFarmData.LastTimePlayerOnline;
                hasPlayedTheGameBefore = true;
                CalculateSecondsOffline();
            }
            else
            {
                hasPlayedTheGameBefore = false;
                
            }
            if (ES3.KeyExists(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm)))
            {
                FarmFieldCurrentlyBuildingData = ES3.Load<FarmFieldCurrentlyBuildingData>(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm));

                if (FarmFieldCurrentlyBuildingData.IsBeingConstructed)
                {
                    if(SecondsOffline > FarmFieldCurrentlyBuildingData.SecondsToBeDone)
                    {
                        MessageDispatcher.Instance.Send(new NewFarmFieldConstructedNotification());

                        Debug.Log("Farm has been constructed");
                    }
                    else
                    {
                        Debug.Log("Farm is not being constructed");
                        MessageDispatcher.Instance.Send(new StartConstructingNewFarmFieldCommand(FarmFieldCurrentlyBuildingData.SecondsToBeDone));

                    }
                }

            }
            else
            {
                FarmFieldCurrentlyBuildingData = new FarmFieldCurrentlyBuildingData(0, false);

            }
            
            StartCoroutine(SaveLoop());
        }

        public void CalculateSecondsOffline()
        {
            if (!hasPlayedTheGameBefore)
            {
                SecondsOffline = 0;
                return;
            } 

            DateTime dateOfPlayerPlayingLastTime = LastTimePlayerOnline;
            var currentDate = DateTime.Now;
            var differenceOfDates = currentDate - dateOfPlayerPlayingLastTime;

            if (differenceOfDates.Days > maxProfitableDaysOffline)
            {
                SecondsOffline = maxProfitableDaysOffline * 60 * 60 * 24;
                return;
            }
            
            int secondsOff = differenceOfDates.Seconds;
            if (secondsOff < 0)
            {
                Debug.LogError($"The time offline is: {secondsOff}! This number should never be negative!");
                secondsOff = 0;
            }
            SecondsOffline = secondsOff;
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
        
        public void SaveOfflineCurrency(InfVal val)
        {
            ES3.Save(GetProperSavingName(FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm), val.ToString());
        }
        
        public void SubtractOfflineCurrency(InfVal val)
        {

            if (!ES3.KeyExists(GetProperSavingName(FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm)))
                return;
            
            InfVal offlineCurrencyValue = InfVal.Parse(
                ES3.Load<string>(GetProperSavingName(FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME,
                    currentNumberOfFarm)));
                
            offlineCurrencyValue -= val;

            ES3.Save(GetProperSavingName(FARM_OFFLINE_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm), offlineCurrencyValue.ToString());
            
        }

        public void SaveTimeOfConstructingFarmField(int timeToFinish)
        {
            FarmFieldCurrentlyBuildingData = new FarmFieldCurrentlyBuildingData(timeToFinish, true);
        }
        
        public void NotifyOfConstructedFarmField()
        {
            FarmFieldCurrentlyBuildingData = new FarmFieldCurrentlyBuildingData(0, false);
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
                CurrencyFarmManger.GetCurrentSuperCurrency());
            
            PlayerFarmData = new PlayerFarmData(DateTime.Now);

        }
        
        private void SaveData()
        {
            ES3.Save(GetProperSavingName(FARM_FIELD_DATAS_SAVING_NAME, currentNumberOfFarm), FarmFieldDatas);
            ES3.Save(GetProperSavingName(FARM_FIELD_CURRENTLY_BUILDING_DATA_SAVING_NAME, currentNumberOfFarm), FarmFieldCurrentlyBuildingData);
            ES3.Save(GetProperSavingName(FARM_GRANARY_DATA_SAVING_NAME, currentNumberOfFarm), FarmGranaryData);
            ES3.Save(GetProperSavingName(FARM_SHOP_DATA_SAVING_NAME, currentNumberOfFarm), FarmShopData);
            ES3.Save(GetProperSavingName(FARM_CURRENCY_DATA_SAVING_NAME, currentNumberOfFarm), FarmCurrencyData);
            ES3.Save(FARM_PLAYER_DATA_SAVING_NAME, PlayerFarmData);
        }
        
        private string GetProperSavingName(string savingName, int numberOfFarm)
        {
            return savingName + numberOfFarm.ToString();
        }

        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch (message)
            {
                case ResetBackendCommand resetBackendCommand:
                {
                    //Resetting in Editor doesn't work since the game is being saved once per second
                    Debug.Log("ResetBackendCommand");
                    ES3.DeleteFile();
                    Application.Quit();
                    break;
                }
            }        
        }
    }
}
