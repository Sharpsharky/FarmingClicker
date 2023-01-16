namespace FarmingClicker.GameFlow.Popup
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class PopupManager : SerializedMonoBehaviour, IMessageReceiver
    {
        //[field: SerializeField, FoldoutGroup("UI")]  private DialoguePopupPanel dialoguePopupPanel;
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Start()
        {
            //ListenedTypes.Add(typeof(DisplayDialogueCommand));

            MessageDispatcher.Instance.RegisterReceiver(this);
        }

        void ClosePopup(PopupPanelBase panelBase)
        {
            Debug.Log($"Closing popup {panelBase}");
            panelBase.OnFinished -= ClosePopup;
            panelBase.gameObject.SetActive(false);
        }
        
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                /*case DisplayLocationLockedCommand command:
                {
                    locationLockedController.OnFinished += ClosePopup;
                    locationLockedController.SetupData(command.Data);

                    break;
                }*/
            }

        }
    }
}
