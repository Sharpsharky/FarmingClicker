namespace FarmingClicker.GameFlow.Interactions.UI.MainCanvas
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Data;
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Messages.Commands.Currency;
    using InfiniteValue;
    using TMPro;
    using UnityEngine;

    public class MainCanvasController : PopupPanelBase, IMessageReceiver
    {

        [SerializeField] private TMP_Text currentCurrencyText;
        [SerializeField] private TMP_Text currentSuperCurrencyText;
        [SerializeField] private TMP_Text currentCurrencyPerSecText;
       
        public List<Type> ListenedTypes { get; } = new List<Type>();

        public override void SetupData(IPopupData data)
        {
            gameObject.SetActive(true);
        }

        public void Initialize()
        {
            ListenedTypes.Add(typeof(SetTextOfCurrentCurrencyCommand));
            ListenedTypes.Add(typeof(SetTextOfCurrentSuperCurrencyCommand));
            ListenedTypes.Add(typeof(SetTextOfCurrentCurrencyPerSecCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            Debug.Log("SetupDataSetupDataSetupData");
            
        }

        private void SetTextOfCurrentCurrency(InfVal currentCurrency)
        {
            Debug.Log("SetTextOfCurrentCurrency");
            currentCurrencyText.text = InfValOperations.DisplayInfVal(currentCurrency);
        }

        private void SetTextOfCurrentSuperCurrency(InfVal currentSuperCurrency)
        {
            currentSuperCurrencyText.text = InfValOperations.DisplayInfVal(currentSuperCurrency);
        }

        private void SetTextOfCurrentCurrencyPerSec(InfVal currentCurrencyPerSec)
        {
            currentCurrencyPerSecText.text = $"{InfValOperations.DisplayInfVal(currentCurrencyPerSec)}/s";
        }


        public void OnMessageReceived(object message)
        {
            if (!ListenedTypes.Contains(message.GetType())) return;
            switch (message)
            {
                case SetTextOfCurrentCurrencyCommand command:
                {
                    SetTextOfCurrentCurrency(command.Amount);
                    break;
                }
                case SetTextOfCurrentSuperCurrencyCommand command:
                {
                    SetTextOfCurrentSuperCurrency(command.Amount);
                    break;
                }
                case SetTextOfCurrentCurrencyPerSecCommand command:
                {
                    SetTextOfCurrentCurrencyPerSec(command.Amount);
                    break;
                }
            }
        }
        
        
    }
}