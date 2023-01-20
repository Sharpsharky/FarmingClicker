namespace FarmingClicker.GameFlow.Sounds.UI
{
    using UnityEngine;
    using Core.Audio.AudioManager;

    public class ButtonClick : MonoBehaviour
    {
        public void ExecuteAudio()
        {
            AudioManager.PlayClip(AudioLibraryData.UI.Click2);
        }
    }
}
