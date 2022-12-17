namespace Utils
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(AspectRatioFitter), typeof(Image))]
    public class AspectRatioFitterSetter : MonoBehaviour
    {
        private void Start()
        {
            var mainTexture = GetComponent<Image>().mainTexture;
            GetComponent<AspectRatioFitter>().aspectRatio =
                mainTexture.width / (float) mainTexture.height;
            Destroy(this);
        }
    }
}