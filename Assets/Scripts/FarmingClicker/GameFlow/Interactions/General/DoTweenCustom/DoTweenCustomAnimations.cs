namespace FarmingClicker.GameFlow.Interactions.General.DoTweenCustom
{
    using DG.Tweening;
    using UnityEngine;
    
    public static class DoTweenCustomAnimations
    {
        private const float blinkTime = 0.25f;
        
        public static void DoBlinkScale(Transform transform, float initialScale, float scaleMultiplication)
        {
            transform.DOScale(initialScale + scaleMultiplication, blinkTime).SetDelay(0).SetEase(Ease.OutBack);
            transform.DOScale(initialScale,blinkTime).SetDelay(blinkTime).SetEase(Ease.OutBack);
        }
    }
}