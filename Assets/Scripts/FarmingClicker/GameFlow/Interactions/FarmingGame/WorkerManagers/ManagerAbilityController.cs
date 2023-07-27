using FarmingClicker.GameFlow.Interactions.General.Time;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    public class ManagerAbilityController : SerializedMonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image cooldownFillImage;
        [SerializeField] private TMP_Text timerText;

        public void SetupTimer(ManagerAbilityCooldown managerAbilityCooldown)
        {
            if (managerAbilityCooldown == ManagerAbilityCooldown.ACTIVE_ABILITY_COOLDOWN)
            {
                cooldownFillImage.fillClockwise = true;
            }
            else
            {
                cooldownFillImage.fillClockwise = false;
            }
            
        }

        public void SetTimer(int currentTime, int maxTime)
        {
            timerText.text = TimeFormatter.FormatTimeToMMSS(currentTime);

            float fillAmount = (float) (maxTime-currentTime) / maxTime;

            cooldownFillImage.fillAmount = fillAmount;

        }
        
        public void TurnOnOffButton(bool onOff)
        {
            button.interactable = onOff;
        }

        public void TurnOnFullCooldownFill()
        {
            cooldownFillImage.fillAmount = 1;
            timerText.text = TimeFormatter.FormatTimeToMMSS(0);

        }
        
        public void TurnOnNoCooldownFill()
        {
            cooldownFillImage.fillAmount = 1;
            timerText.text = TimeFormatter.FormatTimeToMMSS(0);

        }
        
    }
}