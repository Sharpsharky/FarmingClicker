﻿using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry
{
    using Core.Message;
    using Workplaces.Granary;
    using Messages.Commands.Currency;
    using InfiniteValue;
    using TMPro;
    using UnityEngine;
    
    public class LorryAcquireCrops
    {
        private InfVal currentCropCount;
        private InfVal maxCropCount;
        private GranaryController granaryController;
        private WorkplaceController workplaceController;

        [SerializeField] private TMP_Text currentCropCountText;
        
        public void Initialize(WorkplaceController workplaceController, GranaryController granaryController, LorryMovement lorryMovement)
        {
            this.workplaceController = workplaceController;
            this.granaryController = granaryController;
            maxCropCount = granaryController.GetLoadOfCurrentLevelIncrementedBy();
            currentCropCount = 0;
            SetCurrentCropCountText();
            lorryMovement.OnLorryStoppedInGranary += AcquireCrop;
            lorryMovement.OnLorryStoppedInShop += PutCropsToShop;
        }
       
        
        public void AcquireCrop(GranaryController granaryController)
        {
            var finalCrop = currentCropCount + granaryController.GetValueOfCurrentCurrencyInWorkplace();
            InfVal rest = 0;
            var maxCropCount = workplaceController.GetValueOfTransportedCurrency();

            if (finalCrop > maxCropCount)
            {
                rest = finalCrop - maxCropCount;
                finalCrop = maxCropCount;
            }
            
            granaryController.WorkerProperties.currentCurrency = rest;
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