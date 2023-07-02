using DG.Tweening;
using FarmingClicker.GameFlow.Interactions.General.DoTweenCustom;

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

        [SerializeField] private float scaleMultiplicationOfTexts = 0.2f;

        
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

        private void SetTextOfCurrentCurrency(InfVal currentCurrency, InfVal difference)
        {
            Debug.Log($"SetTextOfCurrentCurrency: {currentCurrency}");
            currentCurrencyText.text = InfValOperations.DisplayInfVal(currentCurrency);

            if (difference == new InfVal(0)) return;
            
            DoTweenCustomAnimations.DoBlinkScale(currentCurrencyText.transform, 1, scaleMultiplicationOfTexts);
        }

        private void SetTextOfCurrentSuperCurrency(InfVal currentSuperCurrency, InfVal difference)
        {
            currentSuperCurrencyText.text = InfValOperations.DisplayInfVal(currentSuperCurrency);
            
            if (difference == new InfVal(0)) return;
            
            DoTweenCustomAnimations.DoBlinkScale(currentSuperCurrencyText.transform, 1,scaleMultiplicationOfTexts);
        }

        private void SetTextOfCurrentCurrencyPerSec(InfVal currentCurrencyPerSec)
        {
            currentCurrencyPerSecText.text = $"{InfValOperations.DisplayInfVal(currentCurrencyPerSec)}/s";
            
            DoTweenCustomAnimations.DoBlinkScale(currentCurrencyPerSecText.transform, 1,scaleMultiplicationOfTexts);
        }


        public void OnMessageReceived(object message)
        {
            if (!ListenedTypes.Contains(message.GetType())) return;
            switch (message)
            {
                case SetTextOfCurrentCurrencyCommand command:
                {
                    SetTextOfCurrentCurrency(command.UpdatedAmount, command.Difference);
                    break;
                }
                case SetTextOfCurrentSuperCurrencyCommand command:
                {
                    SetTextOfCurrentSuperCurrency(command.UpdatedAmount, command.Difference);
                    break;
                }
                case SetTextOfCurrentCurrencyPerSecCommand command:
                {
                    SetTextOfCurrentCurrencyPerSec(command.UpdatedAmount);
                    break;
                }
            }
        }
        
        
    }
}