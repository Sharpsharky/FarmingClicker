using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using InfiniteValue;
    using UnityEngine;
    using TMPro;
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
            var finalCrop = currentCropCount + farmFieldController.CurrentCurrency;
            InfVal rest = 0;
            
            if (finalCrop > maxCropCount)
            {
                rest = finalCrop - maxCropCount;
                finalCrop = maxCropCount;
            }
            
            farmFieldController.CurrentCurrency = rest;
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
