namespace Utils.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class PointerEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                       IPointerEnterHandler, IPointerExitHandler
    {
        #region Public Events

        public event Action<PointerEventData> OnPointerDownCallback;
        public event Action<PointerEventData> OnPointerUpCallback;
        public event Action<PointerEventData> OnPointerEnterCallback;
        public event Action<PointerEventData> OnPointerExitCallback;

        #endregion

        #region Pointer event methods

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownCallback?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpCallback?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterCallback?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitCallback?.Invoke(eventData);
        }

        #endregion
    }
}