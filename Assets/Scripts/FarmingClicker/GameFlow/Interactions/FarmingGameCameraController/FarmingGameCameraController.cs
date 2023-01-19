using Core.Message;
using UnityEngine.InputSystem.EnhancedTouch;

namespace FarmingClicker.GameFlow.Interactions.FarmingGameCameraController
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using Core.Message.Interfaces;
    using Messages.Notifications.FarmingGame;

    public class FarmingGameCameraController : MonoBehaviour, IMessageReceiver
    {

        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Awake()
        {
            ListenedTypes.Add(typeof(FarmsLoadedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
        }

        public float scrollSpeed = 10f;
        public float zoomSpeed = 10f;
        public float minSize = 1f;
        public float maxSize = 10f;
        public float zoomSensitivity = 1f;
        public float minY = -10f;
        public float maxY = 10f;
        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }
        void OnEnable()
        {
            TouchSimulation.Enable();
        } 
        void Update()
        {
            Debug.Log($"Input.touchCount: {Input.touchCount}");

            MobileTouch();
            MobileZoom();
            UnityTouch();



        }

        private void MobileTouch()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float y = transform.position.y - touch.deltaPosition.y * scrollSpeed * Time.deltaTime;
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
                float y = transform.position.y - Input.GetAxis("Mouse Y") * scrollSpeed * Time.deltaTime;
                y = Mathf.Clamp(y, minY, maxY);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * scrollSpeed, minSize, maxSize);
        } 

        public void OnMessageReceived(object message)
        {
            if (!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"Navigation manager received load scene command.");

            switch (message)
            {

                case FarmsLoadedNotification farmsLoadedNotification:
                {
                    break;
                }
                
            }
        }
    }
}
