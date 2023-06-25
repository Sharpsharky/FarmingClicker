using System.Collections.Generic;
using FarmingClicker.Data;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Combine;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry;
using InfiniteValue;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields
{
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using FarmsSpawnerManager;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    public class FarmFieldController : WorkplaceController
    {
        [SerializeField] private TMP_Text currentCurrencyText;
        private float leftEdgeOfCombineWay;
        public override void Initialize(FarmCalculationData initialFarmCalculationData, WorkPlaceData workPlaceData
            , GameObject workerPrefab, InitialWorkerProperties initialWorkerProperties)
        {
            workerProperties = new FarmWorkerProperties();
            if(workerProperties is FarmWorkerProperties farmWorkerProperties){
                farmWorkerProperties.SetNumberOfFarm(initialFarmCalculationData.FarmFieldControllers.Count);
                Debug.Log($"SetNumberOfFarm: {initialFarmCalculationData.FarmFieldControllers.Count}");
            }

            leftEdgeOfCombineWay = CalculateLeftEdgeOfCombineWay();  
            base.Initialize(initialFarmCalculationData, workPlaceData, workerPrefab, initialWorkerProperties);
            SetCurrentCurrencyText();
            //StartCoroutine(FakeCurrencyGenerator());
        }

        private float CalculateLeftEdgeOfCombineWay()
        {
            return transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
        
        protected override Vector3 CalculatePositionOfButton()
        {
            Vector3 curPosOfUpgradeButton = gameObject.transform.position;
            curPosOfUpgradeButton.x = initialFarmCalculationData.XOfFirstUpgradeFarmFieldButton;
            return curPosOfUpgradeButton;
        }
        
        private IEnumerator FakeCurrencyGenerator()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                currentCurrency += workerProperties.CroppedCurrency;
                SetCurrentCurrencyText();
            }
        }

        public void SetCurrentCurrency(InfVal amount)
        {
            currentCurrency = amount;
            SetCurrentCurrencyText();
        }

        public void SetCurrentCurrencyText()
        {
            currentCurrencyText.text = currentCurrency.ToPrecision(InGameData.InfValPrecisionDisplayed).ToString(InGameData.MaxDigitsInInfVal);
        }
        protected override WorkerController InitializeWorker()
        {
            var newWorkerController = base.InitializeWorker();

            if (newWorkerController is not CombineController combineController) return null;
            
            combineController.Initialize(this, initialFarmCalculationData, leftEdgeOfCombineWay);
            return newWorkerController;
        }
    }
}
