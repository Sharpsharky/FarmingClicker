using Core.Message;
using TMPro;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.CurrencyFarmManger
{
    using System.Collections.Generic;
    using InfiniteValue;
    using UnityEngine;
    using System;
    using Core.Message.Interfaces;
    using Messages.Commands.Currency;
    using Sirenix.OdinInspector;

    public class CurrencyFarmManger : SerializedMonoBehaviour, IMessageReceiver
    {

        [SerializeField] private TMP_Text currentCurrencyText;
        [SerializeField] private TMP_Text currentSuperCurrencyText;

        private InfVal currentCurrency = 0;
        private InfVal currentSuperCurrency = 0;
        
        private void Awake()
        {
            currentCurrency = 0;
            currentSuperCurrency = 0;

            SetTextOfCurrentCurrency();
            SetTextOfCurrentSuperCurrency();
            
            
            ListenedTypes.Add(typeof(ModifyCurrencyCommand));
            ListenedTypes.Add(typeof(ModifySuperCurrencyCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);        
        }


        private void ModifyCurrentCurrency(InfVal amountToAdd)
        {
            currentCurrency += amountToAdd;
            SetTextOfCurrentCurrency();
        }
        
        private void ModifyCurrentSuperCurrency(InfVal amountToAdd)
        {
            currentSuperCurrency += amountToAdd;
            SetTextOfCurrentSuperCurrency();

        }
        
        private void SetTextOfCurrentCurrency()
        {
            currentCurrencyText.text = currentCurrency.ToString();
        }
        
        private void SetTextOfCurrentSuperCurrency()
        {
            currentSuperCurrencyText.text = currentSuperCurrency.ToString();
        }
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch (message)
            {
                case ModifyCurrencyCommand command:
                {
                    ModifyCurrentCurrency(command.Amount);
                    break;
                }
                case ModifySuperCurrencyCommand command:
                {
                    ModifyCurrentSuperCurrency(command.Amount);
                    break;
                }
            }
        }
    }
}
