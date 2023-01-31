

namespace FarmingClicker.Dialogue
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using GameFlow.Interactions.FarmingGame.Upgrade;
    using GameFlow.Messages.Commands.Popups;
    
    public class PopupManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [field: SerializeField, FoldoutGroup("UI")]  private UpgradePanelController upgradePanelController;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Start()
        {
            ListenedTypes.Add(typeof(DisplayUpgradePanelCommand));

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
                case DisplayUpgradePanelCommand command:
                {
                    upgradePanelController.OnFinished += ClosePopup;
                    upgradePanelController.SetupData(command.data);
                    
                    break;
                }
                
            }

        }
    }
}
