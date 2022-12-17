namespace Core.ViewManager
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected bool isPopup;
        [SerializeField] protected AnimationSequence displayAnimationSequence;
        [SerializeField] protected AnimationSequence hideAnimationSequence;
        private Canvas _canvas;
        protected bool isHideFinished;

        public bool IsPopup => isPopup;

        public Canvas CanvasComponent => _canvas ??= GetComponentInParent<Canvas>();
        public bool IsDisplayFinished { get; protected set; }

        public void Setup()
        {
            Initialize();
            HideInstantly();
        }

        protected virtual void Initialize()
        {
        }

        protected event Action OnDisplayFinished;

        protected virtual void BeforeDisplay()
        {
            IsDisplayFinished = false;
        }

        protected virtual void EndDisplay()
        {
            IsDisplayFinished = true;
            OnDisplayFinished?.Invoke();
        }

        protected virtual void BeforeHide()
        {
            isHideFinished = false;
            IsDisplayFinished = false;
        }

        protected virtual void EndHide()
        {
            isHideFinished = true;
            IsDisplayFinished = false;
        }

        public void Display()
        {
            gameObject.SetActive(true);
            BeforeDisplay();

            if(displayAnimationSequence != null)
            {
                displayAnimationSequence.Play(EndDisplay);
            }
            else
            {
                EndDisplay();
            }
        }

        public void DisplayInstantly()
        {
            gameObject.SetActive(true);
            BeforeDisplay();
            displayAnimationSequence?.PlayInstantly();
            EndDisplay();
        }

        public void Hide()
        {
            BeforeHide();
            if(hideAnimationSequence != null)
            {
                hideAnimationSequence.Play(() =>
                                           {
                                               EndHide();
                                               gameObject.SetActive(false);
                                           });
            }
            else
            {
                EndHide();
                gameObject.SetActive(false);
            }
        }

        public void HideInstantly()
        {
            BeforeHide();
            hideAnimationSequence?.PlayInstantly();
            EndHide();
            gameObject.SetActive(false);
        }

        public void ComepleteDisplaySequece()
        {
            CompleteSequence(displayAnimationSequence);
        }

        public void ComepleteHideSequece()
        {
            CompleteSequence(hideAnimationSequence);
        }

        private void CompleteSequence(AnimationSequence animationSequence)
        {
            animationSequence?.Complete();
        }
    }
}