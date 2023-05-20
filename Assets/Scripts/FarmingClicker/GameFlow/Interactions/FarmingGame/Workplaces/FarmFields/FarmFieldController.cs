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

        public override void Initialize(FarmCalculationData initialFarmCalculationData, WorkPlaceData workPlaceData, GameObject workerPrefab)
        {
            
            base.Initialize(initialFarmCalculationData, workPlaceData, workerPrefab);
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
                workerProperties.currentCurrency += workerProperties.valueOfCroppedCurrency;
                SetCurrentCurrencyText();
            }
        }

        public void SetCurrentCurrency(InfVal amount)
        {
            workerProperties.currentCurrency = amount;
            SetCurrentCurrencyText();
        }

        public void SetCurrentCurrencyText()
        {
            currentCurrencyText.text = workerProperties.currentCurrency.ToString();
        }
        
    }
}
