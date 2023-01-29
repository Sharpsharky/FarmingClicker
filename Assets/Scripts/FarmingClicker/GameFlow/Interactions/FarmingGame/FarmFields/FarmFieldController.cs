using TMPro;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields
{
    using InfiniteValue;
    using UnityEngine;
    using System.Collections;

    public class FarmFieldController : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentCurrencyText;

        
        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfCroppedCurrency = 0;

        
        public void InitializeFarmField(int upgradeLevel, int numberOfWorkers, InfVal currentCurrency, InfVal valueOfCroppedCurrency)
        {
            this.upgradeLevel = upgradeLevel;
            this.numberOfWorkers = numberOfWorkers;
            this.currentCurrency = currentCurrency;
            this.valueOfCroppedCurrency = valueOfCroppedCurrency;
            
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
