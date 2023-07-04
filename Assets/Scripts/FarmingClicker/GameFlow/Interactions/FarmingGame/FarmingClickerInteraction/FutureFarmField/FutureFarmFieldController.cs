using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.FutureFarmField
{
    using Core.Message;
    using FarmingClicker.Data.Popup;
    using Messages.Commands.Popups;
    using InfiniteValue;
    using UnityEngine;
    using UnityEngine.UI;
    using FarmsSpawnerManager;
    using FarmingClicker.GameFlow.Messages.Commands.NewField;
    using Sirenix.OdinInspector;
    using TMPro;
    using Data;
    using System.Collections;

    public class FutureFarmFieldController : SerializedMonoBehaviour
    {
        [SerializeField,BoxGroup("Future farm field")] private GameObject upgradeButtonParent;
        [SerializeField,BoxGroup("Future farm field")] private Button upgradeButton;
        
        [SerializeField,BoxGroup("Under construction")] private GameObject boostButtonParent;
        [SerializeField,BoxGroup("Under construction")] private GameObject timerParent;
        [SerializeField,BoxGroup("Under construction")] private Button boostButton;
        [SerializeField,BoxGroup("Under construction")] private TMP_Text timer;

        private InfVal priceToBuyTheNextFarmField = new InfVal(0, InGameData.InfValPrecision);
        private int timeOfFarmConstruction;
        private FarmCalculationData initialFarmCalculationData;
        
        public void Initialize(FarmCalculationData initialFarmCalculationData)
        {
            SetDataForFutureFarmField(priceToBuyTheNextFarmField,timeOfFarmConstruction);
            this.initialFarmCalculationData = initialFarmCalculationData;
            SetPositionOfButton();
            upgradeButton.onClick.AddListener(OpenBuyNewFieldPopUp);
            upgradeButton.onClick.AddListener(OpenSpeedUpFarmConstructing);
            
            upgradeButtonParent.SetActive(true);
            boostButtonParent.SetActive(false);
            timerParent.SetActive(false);
            
        }

        public void PutFutureFarmInNewPosition()
        {
            var posOfFutureFarm = transform.position;
            posOfFutureFarm.y -= initialFarmCalculationData.DistanceBetweenStops;
            transform.position = posOfFutureFarm;
        }

        public void SetDataForFutureFarmField(InfVal priceToBuyTheNextFarmField, int timeOfFarmConstruction)
        {
            this.priceToBuyTheNextFarmField = priceToBuyTheNextFarmField;
            this.timeOfFarmConstruction = timeOfFarmConstruction;
        }

        public InfVal GetPrice()
        {
            return priceToBuyTheNextFarmField;
        }
        
        private void SetPositionOfButton()
        {
            Vector3 curPosOfUpgradeButton = gameObject.transform.position;
            curPosOfUpgradeButton.x = initialFarmCalculationData.XOfFirstUpgradeFarmFieldButton;
            upgradeButton.transform.parent.position = curPosOfUpgradeButton;
        }
        
        private void OpenBuyNewFieldPopUp()
        {
            var buyNewFieldPopupData = new BuyNewFieldPopupData(priceToBuyTheNextFarmField, timeOfFarmConstruction);
            MessageDispatcher.Instance.Send(new DisplayBuyNewFieldPanelCommand(buyNewFieldPopupData));
        }

        private void OpenSpeedUpFarmConstructing()
        {
            
        }
        
        
        public void StartConstructingFarmField(int timeOfConstruction)
        {
            Debug.Log($"StartConstructingFarmField: {timeOfConstruction}");
            TurnOnTimer(timeOfConstruction);
        }
        
        private void TurnOnTimer(int timeOfConstruction)
        {
            upgradeButtonParent.SetActive(false);
            boostButtonParent.SetActive(true);
            timerParent.SetActive(true);
            
            timer.text = timeOfConstruction.ToString();
            
            StartCoroutine(CountTime(timeOfConstruction));
        }

        private IEnumerator CountTime(int timeOfConstruction)
        {
            while (timeOfConstruction >= 0)
            {
                yield return new WaitForSeconds(1);
                timeOfConstruction--;
                timer.text = timeOfConstruction.ToString();
                LoadDataFarmManager.instance.SaveTimeOfConstructingFarmField(timeOfConstruction);
            }
            
            MessageDispatcher.Instance.Send(new NewFarmFieldConstructedNotification());
            LoadDataFarmManager.instance.NotifyOfConstructedFarmField();
            
            yield return null;
            
        }
        
        
    }
}
