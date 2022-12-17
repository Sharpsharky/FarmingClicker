namespace Utils
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class CameraContentFitter : MonoBehaviour
    {
        #region Inspector

        [SerializeField, InfoBox("Used for orthographic camera.")]
        private float sceneWidth = 10;

        [SerializeField, InfoBox("Used for perspective camera.")]
        private float horizontalFoV = 90.0f;

        #endregion

        #region Private Variables

        private Camera targetCamera;
        private const float VERTICAL_FOV_FACTOR = 2f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            targetCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            if(targetCamera.orthographic)
            {
                UpdateOrthographic();
                return;
            }

            UpdatePerspective();
        }

        #endregion

        #region Private Methods

        private void UpdateOrthographic()
        {
            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

            targetCamera.orthographicSize = desiredHalfHeight;
        }

        private void UpdatePerspective()
        {
            float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);
            float halfHeight = halfWidth * Screen.height / Screen.width;
            float verticalFoV = VERTICAL_FOV_FACTOR * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

            targetCamera.fieldOfView = verticalFoV;
        }

        #endregion
    }
}