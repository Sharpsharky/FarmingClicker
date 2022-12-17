namespace Core.ViewManager
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ViewManager : MonoBehaviour, IViewManager, IInitializable
    {
        [SerializeField] private View[] viewsReferences;

        private readonly Dictionary<Type, View> viewsDictionary = new();

        private Canvas canvas;
        private View currentPopupView;
        private View currentView;
        private bool isInitialized;
        private View previousView;

        public void Initialize()
        {
            if(isInitialized)
            {
                return;
            }

            canvas = GetComponentInParent<Canvas>();

            foreach(var view in viewsReferences)
            {
                Debug.Log($"[{name}] Setting up view {view.name}...");
                viewsDictionary.Add(view.GetType(), view);
                view.Setup();
            }

            isInitialized = true;
        }

        public T GetView<T>() where T : View
        {
            var givenType = typeof(T);

            if(viewsDictionary.ContainsKey(givenType))
            {
                return viewsDictionary[givenType] as T;
            }

            return default;
        }

        public T SwitchView<T>() where T : View
        {
            var newView = GetView<T>();
            if(currentView == null)
            {
                currentView = newView;
                currentView.DisplayInstantly();
                return currentView as T;
            }

            if(newView == currentView)
            {
                return currentView as T;
            }

            if(newView.IsPopup)
            {
                currentPopupView = newView;
                PlaySwitchSequence(currentView, currentPopupView);
                return currentPopupView as T;
            }

            previousView?.ComepleteHideSequece();
            currentView?.ComepleteDisplaySequece();

            previousView = currentView;
            currentView = newView;
            PlaySwitchSequence(previousView, currentView);
            return currentView as T;
        }

        public View GetCurrentView()
        {
            return currentView;
        }

        public View GetPreviousView()
        {
            return previousView;
        }

        public void SetRenderCamera(Camera cameraToSet)
        {
            canvas.worldCamera = cameraToSet;
        }

        public void SetRenderMode(RenderMode renderMode)
        {
            canvas.renderMode = renderMode;
        }

        private static void PlaySwitchSequence(View viewToHide, View viewToDisplay)
        {
            bool isViewToDisplayPopup = !viewToDisplay.IsPopup;
            if(isViewToDisplayPopup)
            {
                viewToHide.Hide();
            }

            if(viewToHide.IsPopup)
            {
                return;
            }

            if(isViewToDisplayPopup)
            {
                viewToDisplay.transform.SetAsFirstSibling();
            }
            else
            {
                viewToDisplay.transform.SetAsLastSibling();
            }

            viewToDisplay.Display();
        }
    }
}