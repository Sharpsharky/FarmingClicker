﻿namespace Utils
{
    using UnityEngine;

    public class FillQuadToCamera : MonoBehaviour
    {
        [SerializeField] private bool useOnUpdate;
        [field: SerializeField] private new Camera camera { get; set; }

        private void Start()
        {
            AdjustCamera();
        }

        private void Update()
        {
            if(useOnUpdate)
            {
                AdjustCamera();
            }
        }

        private void OnValidate()
        {
            AdjustCamera();
        }

        private void AdjustCamera()
        {
            if(camera == null)
            {
                return;
            }

            float pos = camera.farClipPlane - 1.1f;
            transform.position = camera.transform.position + camera.transform.forward * pos;
            transform.rotation = camera.transform.rotation;
            float h = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f;
            transform.localScale = new Vector3(h * camera.aspect, h, 0f);
        }
    }
}