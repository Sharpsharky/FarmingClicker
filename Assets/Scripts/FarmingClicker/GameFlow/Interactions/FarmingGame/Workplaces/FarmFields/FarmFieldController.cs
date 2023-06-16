using FarmingClicker.Data;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
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

        public override void Initialize(FarmCalculationData initialFarmCalculationData, GameObject workerPrefab, 
            InitialWorkerProperties initialWorkerProperties, int initialLevel)
        {
            workerProperties = new FarmWorkerProperties();
            if(workerProperties is FarmWorkerProperties farmWorkerProperties){
                farmWorkerProperties.SetNumberOfFarm(initialFarmCalculationData.FarmFieldControllers.Count);
                Debug.Log($"SetNumberOfFarm: {initialFarmCalculationData.FarmFieldControllers.Count}");
            }
            
            base.Initialize(initialFarmCalculationData, workerPrefab, initialWorkerProperties, initialLevel);
            Debug.Log("Initialize + FarmFieldController_>_>_>_");
            SetCurrentCurrencyText();
            StartCoroutine(FakeCurrencyGenerator());
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
            currentCurrencyText.text = currentCurrency.ToString(InGameData.MaxDigitsInInfVal);
        }
        
    }
}
