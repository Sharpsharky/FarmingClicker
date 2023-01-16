namespace FarmingClicker.GameFlow.Popup
{
    using System;
    using Sirenix.OdinInspector;
    public abstract class PopupPanelBase : SerializedMonoBehaviour
    {
        public abstract void SetupData(IPopupData data);

        public virtual void Close()
        {
            OnFinished?.Invoke(this);
        }

        public event Action<PopupPanelBase> OnFinished;
    }
}
