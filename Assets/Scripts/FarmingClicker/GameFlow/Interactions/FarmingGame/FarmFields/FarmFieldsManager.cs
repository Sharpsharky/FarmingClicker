using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields
{
    using System.Collections.Generic;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    using UnityEngine;
    using InfiniteValue;

    public class FarmFieldsManager : MonoBehaviour
    {
        private List<FarmFieldData> farmFields = new List<FarmFieldData>();
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();

        public void Initialize(List<FarmFieldController> farmFieldControllers)
        {
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
        
    }
}
