namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Upgrade
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Upgrade Statistic Component", menuName = "Upgrade/Statistic Component")]
    public class UpgradeStatisticComponents : ScriptableObject
    {
        [SerializeField] private string titleKey;
        [SerializeField] private Sprite icon;

        public string GetTitle()
        {
            return I2.Loc.LocalizationManager.GetTranslation(titleKey);
        }

        public Sprite GetIcon()
        {
            return icon;
        }
        
    }
}
