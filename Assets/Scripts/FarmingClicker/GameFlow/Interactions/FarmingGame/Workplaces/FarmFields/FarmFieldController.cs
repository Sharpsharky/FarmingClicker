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
            SetPositionOfButton();
            StartCoroutine(FakeCurrencyGenerator());
        }

        private void SetPositionOfButton()
        {
            Vector3 curPosOfUpgradeButton = gameObject.transform.position;
            curPosOfUpgradeButton.x = initialFarmCalculationData.XOfFirstUpgradeFarmFieldButton;
            upgradeButton.transform.position = curPosOfUpgradeButton;
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
