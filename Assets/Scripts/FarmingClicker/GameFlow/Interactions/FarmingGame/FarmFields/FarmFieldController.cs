using Core.Message;
using FarmingClicker.Data.Popup;
using FarmingClicker.GameFlow.Interactions.FarmingGame.General;
using FarmingClicker.GameFlow.Messages.Commands.Popups;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields
{
    using InfiniteValue;
    using UnityEngine;
    using System.Collections;
    using TMPro;

    public class FarmFieldController : MonoBehaviour, IFarmWorkerControllable
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text currentCurrencyText;
        [SerializeField] private string title;

        private int upgradeLevel = 0;
        private int numberOfWorkers = 0;
        private InfVal currentCurrency = 0;
        private InfVal valueOfCroppedCurrency = 0;

        
        public void InitializeFarmField(int upgradeLevel, int numberOfWorkers, InfVal currentCurrency, InfVal valueOfCroppedCurrency, float xPosOfUpgradeButton)
        {
            this.upgradeLevel = upgradeLevel;
            this.numberOfWorkers = numberOfWorkers;
            this.currentCurrency = currentCurrency;
            SetValueOfCroppedCurrency(valueOfCroppedCurrency);
            DisplayUpgradeButton(xPosOfUpgradeButton);

            StartCoroutine(FakeCurrencyGenerator());
        }

        public InfVal SetValueOfCroppedCurrency(InfVal valueOfCroppedCurrency)
        {
            this.valueOfCroppedCurrency = valueOfCroppedCurrency;

            return valueOfCroppedCurrency;
        }
        
        private IEnumerator FakeCurrencyGenerator()
        {
            yield return new WaitForSeconds(3f);
            currentCurrency += valueOfCroppedCurrency;
            currentCurrencyText.text = currentCurrency.ToString();
            yield return null;
        }

        public void DisplayUpgradeButton(float buttonXPos)
        {
            Vector3 pos = new Vector3(buttonXPos, transform.position.y, 0);
            upgradeButton.gameObject.transform.position = pos;
            upgradeButton.onClick.AddListener(DisplayUpgrade);
        }
        
        private void DisplayUpgrade()
        {
            UpgradeDisplayPopupData data = new UpgradeDisplayPopupData(this, title, currentCurrency);
            
            MessageDispatcher.Instance.Send(new DisplayUpgradePanelCommand(data));

        }
        
        public void BuyUpgrade(int amount)
        {
            upgradeLevel += amount;
        }
    }
}
