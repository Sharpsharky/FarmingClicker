namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields
{
    using System.Collections;
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using Messages.Commands.Popups;
    using InfiniteValue;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using FarmsSpawnerManager;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    public class FarmFieldController : WorkplaceController
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text currentCurrencyText;
        [SerializeField] private string title;

        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfCroppedCurrency = 0;

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
