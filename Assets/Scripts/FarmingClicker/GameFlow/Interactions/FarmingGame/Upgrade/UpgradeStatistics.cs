namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Upgrade
{
    using TMPro;
    using UnityEngine.UI;
    using UnityEngine;

    public class UpgradeStatistics : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text currentLevelText;
        [SerializeField] private TMP_Text nextLevelText;

        public void InitializeStatistic(Sprite icon, string nameText, string currentLevelText, string nextLevelText)
        {
            this.icon.sprite = icon;
            this.nameText.text = nameText;
            this.currentLevelText.text = currentLevelText;
            this.nextLevelText.text = nextLevelText;
        }
        
    }
}