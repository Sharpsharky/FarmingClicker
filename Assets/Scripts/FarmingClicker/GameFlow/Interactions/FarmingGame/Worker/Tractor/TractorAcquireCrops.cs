using FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;
using InfiniteValue;
using TMPro;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor
{
    public class TractorAcquireCrops : MonoBehaviour
    {
        private InfVal currentCropCount;
        private WorkplaceController workplaceController;

        [SerializeField] private TMP_Text currentCropCountText;
        
        public void Initialize(WorkplaceController workplaceController, TractorMovement tractorMovement)
        {
            this.workplaceController = workplaceController;
            currentCropCount = 0;
            SetCurrentCropCountText();
            tractorMovement.OnTractorStoppedOnFarmField += AcquireCrop;
        }
       
        
        public void AcquireCrop(FarmFieldController farmFieldController)
        {
            var finalCrop = currentCropCount + farmFieldController.CurrentCurrency;
            InfVal rest = 0;
            var maxCropCount = workplaceController.WorkerProperties.MaxTransportedCurrency;
            Debug.Log($"finalCrop: {finalCrop}, maxCropCount: {maxCropCount}");
            if (finalCrop > maxCropCount)
            {
                rest = finalCrop - maxCropCount;
                finalCrop = maxCropCount;
                Debug.Log($"rest: {rest}, finalCrop: {finalCrop}");

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
