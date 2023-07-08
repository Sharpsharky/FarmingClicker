using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;
using FarmingClicker.GameFlow.Interactions.UI.MainCanvas;

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
        [field: SerializeField, FoldoutGroup("UI")]  private MainCanvasController mainCanvasController;
        [field: SerializeField, FoldoutGroup("UI")]  private SelectManagerController selectManagerController;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Start()
        {
            ListenedTypes.Add(typeof(DisplayUpgradePanelCommand));
            ListenedTypes.Add(typeof(DisplayOptionsPanelCommand));
            ListenedTypes.Add(typeof(DisplayBuyNewFieldPanelCommand));
            ListenedTypes.Add(typeof(DisplayProfitPanelCommand));
            ListenedTypes.Add(typeof(DisplayMainCanvasCommand));
            ListenedTypes.Add(typeof(DisplaySelectManagerCommand));

            MessageDispatcher.Instance.RegisterReceiver(this);
            
            MessageDispatcher.Instance.Send(new DisplayMainCanvasCommand(null));

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
                case DisplayProfitPanelCommand command:
                {
                    Debug.Log($"DisplayProfitPanelCommand: {command.data.Amount}");
                    if(command.data.Amount <= 0) return;
                    
                    profitController.OnFinished += ClosePopup;
                    profitController.SetupData(command.data);
                    
                    break;
                }
                case DisplayMainCanvasCommand command:
                {
                    mainCanvasController.OnFinished += ClosePopup;
                    mainCanvasController.SetupData(command.data);
                    break;
                }
                case DisplaySelectManagerCommand command:
                {
                    selectManagerController.OnFinished += ClosePopup;
                    selectManagerController.SetupData(command.data);
                    break;
                }
            }

        }
    }
}
