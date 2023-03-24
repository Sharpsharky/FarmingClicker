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
                currentCurrency += valueOfCroppedCurrency;
                SetCurrentCurrencyText();
            }

            yield return null;
        }

        public void TakeAmountOfCrops(InfVal amount)
        {
            currentCurrency -= CalculateAmountOfCropsToTake(amount);
            SetCurrentCurrencyText();
        }

        private void SetCurrentCurrencyText()
        {
            currentCurrencyText.text = currentCurrency.ToString();
        }
        
        private InfVal CalculateAmountOfCropsToTake(InfVal amount)
        {
            if (amount <= currentCurrency)
            {
                return amount;
            }
            else
            {
                return amount - currentCurrency;
            }
            
            
        }

        
    }
}
