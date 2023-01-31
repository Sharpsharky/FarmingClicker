namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields
{
    using System.Collections.Generic;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    using UnityEngine;
    using InfiniteValue;
    using System;
    using Core.Message;
    using Core.Message.Interfaces;
    using LoadData;
    using Messages.Commands.Upgrades;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;
    
    public class FarmFieldsManager : MonoBehaviour, IMessageReceiver
    {
        private List<FarmFieldData> farmFields = new List<FarmFieldData>();
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();

        public void Initialize(List<FarmFieldController> farmFieldControllers)
        {
            
            ListenedTypes.Add(typeof(BuyFarmFieldUpgradeCommand));

            MessageDispatcher.Instance.RegisterReceiver(this);
            
            this.farmFieldControllers = farmFieldControllers;
            farmFields = LoadDataFarmManager.instance.FarmFieldDatas;
            
            DistributeValuesAcrossFarmFields();
        }

        private void DistributeValuesAcrossFarmFields()
        {
            for (int i = 0; i < farmFields.Count; i++)
            {
                farmFieldControllers[i].InitializeFarmField(farmFields[i].upgradeLevel, farmFields[i].numberOfWorkers, 
                    0,CalculateValueOfCroppedCurrency(farmFields[i].upgradeLevel));
            }
        }
        
        private InfVal CalculateValueOfCroppedCurrency(int upgradeLevel)
        {
            return 1 * upgradeLevel;
        }

        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case BuyFarmFieldUpgradeCommand buyFarmFieldUpgradeCommand:
                {
                    farmFields[buyFarmFieldUpgradeCommand.FarmIndex].upgradeLevel +=
                        buyFarmFieldUpgradeCommand.AmountOfBoughtUpgrades;

                    var currentValueOfCroppedCurrency = farmFieldControllers[buyFarmFieldUpgradeCommand.FarmIndex].SetValueOfCroppedCurrency(
                        CalculateValueOfCroppedCurrency(farmFields[buyFarmFieldUpgradeCommand.FarmIndex].upgradeLevel));
                    
                    MessageDispatcher.Instance.Send(new ChangeStatisticsOfUpgradeNotification(currentValueOfCroppedCurrency));
                    
                    break;
                }
                
            }
        }
    }
}
