namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields
{
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using System.Collections.Generic;
    using FarmsSpawnerManager;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    public class FarmFieldController : WorkplaceController
    {
        [SerializeField] private TMP_Text currentCurrencyText;

        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkPlaceData> workPlaceDataList, GameObject workerPrefab)
        {
            base.Initialize(initialFarmCalculationData, workPlaceDataList, workerPrefab);
            StartCoroutine(FakeCurrencyGenerator());
        }
        
        private IEnumerator FakeCurrencyGenerator()
        {
            yield return new WaitForSeconds(3f);
            currentCurrency += valueOfCroppedCurrency;
            currentCurrencyText.text = currentCurrency.ToString();
            yield return null;
        }


        
    }
}
