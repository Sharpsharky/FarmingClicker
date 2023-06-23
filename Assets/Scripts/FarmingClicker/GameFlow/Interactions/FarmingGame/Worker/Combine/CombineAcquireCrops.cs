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
        private InfVal currentCropCount;
        private FarmFieldController farmFieldController;

        private bool isMoving = false;
        
        [SerializeField] private TMP_Text currentCropCountText;
        
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
            StartCoroutine(AcquireCrops());
            isMoving = true;
        }
        private void StopGettingCrops()
        {
            isMoving = false;
        }
        private IEnumerator AcquireCrops()
        {
            while (isMoving)
            {
                yield return new WaitForSeconds(0.5f);
                currentCropCount += farmFieldController.WorkerProperties.CroppedCurrency;
                SetCurrentCropCountText();
            }
            yield return null;
        }
        
        private void GiveCrop()
        {
            farmFieldController.CurrentCurrency += farmFieldController.WorkerProperties.CroppedCurrency;
            currentCropCount = 0;
            SetCurrentCropCountText();
            farmFieldController.SetCurrentCurrencyText();
        }
        
        private void SetCurrentCropCountText()
        {
            currentCropCountText.text = currentCropCount.ToString(InGameData.MaxDigitsInInfVal);
        }

    }

}