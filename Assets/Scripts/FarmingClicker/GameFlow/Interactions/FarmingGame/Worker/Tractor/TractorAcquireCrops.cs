namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using InfiniteValue;
    using UnityEngine;
    using TMPro;
    using Workplaces.FarmFields;

    public class TractorAcquireCrops : MonoBehaviour
    {
        private InfVal currentCropCount;
        private InfVal maxCropCount;
        
        [SerializeField] private TMP_Text currentCropCountText;
        
        public void Initialize(InfVal maxCropCount, TractorMovement tractorMovement)
        {
            this.maxCropCount = maxCropCount;
            currentCropCount = 0;
            SetCurrentCropCountText();
            tractorMovement.OnTractorStoppedOnFarmField += AcquireCrop;
        }
       
        
        public void AcquireCrop(FarmFieldController farmFieldController)
        {
            var finalCrop = currentCropCount + farmFieldController.GetValueOfCurrentCurrencyInWorkplace();
            InfVal rest = 0;
            
            if (finalCrop > maxCropCount)
            {
                rest = finalCrop - maxCropCount;
                finalCrop = maxCropCount;
            }
            
            farmFieldController.WorkerProperties.currentCurrency = rest;
            currentCropCount = finalCrop;

            SetCurrentCropCountText();
            farmFieldController.SetCurrentCurrency(rest);
            
        }
        
        private void SetCurrentCropCountText()
        {
            currentCropCountText.text = currentCropCount.ToString();
        }

        public InfVal GetCurrentCropAndResetIt()
        {
            InfVal curCrop = currentCropCount;
            currentCropCount = 0;
            SetCurrentCropCountText();
            return curCrop;
        }
        
    }
}
