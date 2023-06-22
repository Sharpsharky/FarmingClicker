using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingGameCamera
{
    public class FarmingGameCameraController : MonoBehaviour
    {
        [SerializeField] private float scrollSpeedEditor = 10f;
        [SerializeField] private float scrollSpeedApp = 0.003f;
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float minSize = 1f;
        [SerializeField] private float maxSize = 10f;
        [SerializeField] private float zoomSensitivity = 1f;
        [SerializeField] private float minY = -10f;
        [SerializeField] private float maxY;

        private bool isInitialized = false;
        private Camera cam;

        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        public void Initialize()
        {
            cam = GetComponent<Camera>();
            maxY = transform.position.y;
            enabled = true;
        }
        
        void Update()
        {
            MobileTouch();
            MobileZoom();
            
            #if UNITY_EDITOR
            UnityTouch();
            #endif
        }

        private void MobileTouch()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float y = transform.position.y - touch.deltaPosition.y * scrollSpeedApp;
                    y = Mathf.Clamp(y, minY, maxY);
                    transform.position = new Vector3(transform.position.x, y, transform.position.z);
                }
            }
        }

        private void MobileZoom()
        {
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                cam.orthographicSize += deltaMagnitudeDiff * zoomSensitivity * Time.deltaTime;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
            }
        }

        private void UnityTouch()
        {
            if (Input.GetMouseButton(0))
            {
                float y = transform.position.y - Input.GetAxis("Mouse Y") * scrollSpeedEditor * Time.deltaTime;
                y = Mathf.Clamp(y, minY, maxY);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * scrollSpeedEditor, minSize, maxSize);
        } 

    }
}
