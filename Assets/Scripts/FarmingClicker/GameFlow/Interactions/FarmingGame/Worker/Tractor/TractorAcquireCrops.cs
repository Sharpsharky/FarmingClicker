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

        public void AcquireCrop(InfVal cropToAdd)
        {
            currentCropCount += cropToAdd;
            if (currentCropCount > maxCropCount) currentCropCount = maxCropCount;
            
            
        }

        private void DisplayCurrentCropCount()
        {
            currentCropCountText.text = currentCropCount.ToString();
        }
        
    }
}
