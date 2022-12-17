using TMPro;
using UnityEngine;

namespace Utils
{
    public class FPSShower : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        private float fps = 30f;


        private void Update()
        {
            float newFPS = 1.0f / Time.smoothDeltaTime;
            fps = Mathf.Lerp(fps, newFPS, 0.0005f);
            fpsText.text = $"FPS: {newFPS}";
        }
    }
}