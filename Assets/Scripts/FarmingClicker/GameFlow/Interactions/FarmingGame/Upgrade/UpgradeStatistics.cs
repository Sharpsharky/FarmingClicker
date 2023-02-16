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
        [SerializeField] private TMP_Text valX5Text;
        [SerializeField] private TMP_Text valX10Text;

        public void InitializeStatistic(Sprite icon, string nameText, string currentLevelText, string valX5Text, string valX10Text)
        {
            this.icon.sprite = icon;
            this.nameText.text = nameText;
            this.currentLevelText.text = currentLevelText;
            this.valX5Text.text = valX5Text;
            this.valX10Text.text = valX10Text;
        }

        public void SetNewLevelValues(string currentLevelText, string valX5Text, string valX10Text)
        {
            this.currentLevelText.text = currentLevelText;
            this.valX5Text.text = valX5Text;
            this.valX10Text.text = valX10Text;
        }
        
    }
}