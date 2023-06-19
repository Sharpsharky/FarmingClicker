
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
    using GameFlow.Interactions.FarmingGame.NewField;
    using GameFlow.Interactions.FarmingGame.Options;
    using GameFlow.Interactions.FarmingGame.Profit;

    public class PopupManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [field: SerializeField, FoldoutGroup("UI")]  private UpgradePanelController upgradePanelController;
        [field: SerializeField, FoldoutGroup("UI")]  private BuyNewFieldController buyNewFieldController;
        [field: SerializeField, FoldoutGroup("UI")]  private OptionsController optionsController;
        [field: SerializeField, FoldoutGroup("UI")]  private ProfitController profitController;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Start()
        {
            ListenedTypes.Add(typeof(DisplayUpgradePanelCommand));
            ListenedTypes.Add(typeof(DisplayOptionsPanelCommand));
            ListenedTypes.Add(typeof(DisplayBuyNewFieldPanelCommand));

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
                case DisplayBuyNewFieldPanelCommand command:
                {
                    buyNewFieldController.OnFinished += ClosePopup;
                    buyNewFieldController.SetupData(command.data);
                    
                    break;
                }
                case DisplayOptionsPanelCommand command:
                {
                    optionsController.OnFinished += ClosePopup;
                    optionsController.SetupData(command.data);
                    
                    break;
                }
            }

        }
    }
}
