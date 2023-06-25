namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Combine
{
    using Data;
    using Workplaces.FarmFields;
    using InfiniteValue;
    using TMPro;
    using UnityEngine;
    using System.Collections;
    public class CombineAcquireCrops : MonoBehaviour
    {

        [SerializeField] private TMP_Text currentCropCountText;
        [SerializeField] private float harvestingRate = 0.5f;
        
        private InfVal currentCropCount;
        private FarmFieldController farmFieldController;

        private bool isMoving = false;
        
        
        public void Initialize(FarmFieldController farmFieldController, CombineMovement combineMovement)
        {
            this.farmFieldController = farmFieldController;
            currentCropCount = 0;
            SetCurrentCropCountText();
            combineMovement.OnCombineStoppedOnChest += GiveCrop;
            combineMovement.OnCombineStartMoving += StartGettingCrops;
            combineMovement.OnCombineStoppedMoving += StopGettingCrops;
        }

        private void StartGettingCrops()
        {
            isMoving = true;
            StartCoroutine(AcquireCrops());
        }
        private void StopGettingCrops()
        {
            isMoving = false;
        }
        private IEnumerator AcquireCrops()
        {

            while (isMoving)
            {

                yield return new WaitForSeconds(harvestingRate);
                
                if(!isMoving) yield return null;
                
                currentCropCount += farmFieldController.WorkerProperties.CroppedCurrency;
                SetCurrentCropCountText();
            }
            yield return null;
        }
        
        private void GiveCrop()
        {
            farmFieldController.CurrentCurrency += currentCropCount;
            currentCropCount = 0;
            SetCurrentCropCountText();
            farmFieldController.SetCurrentCurrencyText();
        }
        
        private void SetCurrentCropCountText()
        {
            currentCropCountText.text = InfValOperations.DisplayInfVal(currentCropCount);
        }

    }

}