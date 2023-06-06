namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry
{
    using Core.Message;
    using Workplaces.Granary;
    using Messages.Commands.Currency;
    using InfiniteValue;
    using TMPro;
    using UnityEngine;
    using Workplaces;

    public class LorryAcquireCrops
    {
        private InfVal currentCropCount;
        private WorkplaceController workplaceController;

        [SerializeField] private TMP_Text currentCropCountText;
        
        public void Initialize(WorkplaceController workplaceController, LorryMovement lorryMovement)
        {
            this.workplaceController = workplaceController;
            currentCropCount = 0;
            SetCurrentCropCountText();
            lorryMovement.OnLorryStoppedInGranary += AcquireCrop;
            lorryMovement.OnLorryStoppedInShop += PutCropsToShop;
        }
        
        public void AcquireCrop(GranaryController granaryController)
        {
            var finalCrop = currentCropCount + granaryController.CurrentCurrency;
            InfVal rest = 0;
            var maxCropCount = workplaceController.WorkerProperties.MaxTransportedCurrency;

            if (finalCrop > maxCropCount)
            {
                rest = finalCrop - maxCropCount;
                finalCrop = maxCropCount;
            }
            
            granaryController.CurrentCurrency = rest;
            currentCropCount = finalCrop;
            
            SetCurrentCropCountText();
            granaryController.SetCurrentCurrency(rest);
            
            Debug.Log($"Max crop count: {maxCropCount}, Acquire crop: {currentCropCount} Rest: {rest} ");

        }

        private void PutCropsToShop()
        {
            MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(currentCropCount));
            currentCropCount = 0;
            SetCurrentCropCountText();
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