namespace FarmingClicker.Dialogue.DialoguePanelControllers
{
    using System;
    using DialogueDataTypes;
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
