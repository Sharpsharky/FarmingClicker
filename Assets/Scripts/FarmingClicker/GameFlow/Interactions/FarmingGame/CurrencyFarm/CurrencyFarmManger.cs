namespace FarmingClicker.GameFlow.Interactions.FarmingGame.CurrencyFarm
{
    using Data;
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Messages.Commands.Currency;
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using TMPro;
    using UnityEngine;
    public class CurrencyFarmManger : SerializedMonoBehaviour, IMessageReceiver
    {

        [SerializeField] private TMP_Text currentCurrencyText;
        [SerializeField] private TMP_Text currentSuperCurrencyText;
        [SerializeField] private TMP_Text currentCurrencyPerSecText;

        private static InfVal currentCurrency = 0;
        private static InfVal currentSuperCurrency = 0;
        private static InfVal currentCurrencyPerSec = 0;
        
        private void Awake()
        {
            currentCurrency = 0;
            currentSuperCurrency = 0;
            currentCurrencyPerSec = 0;

            SetTextOfCurrentCurrency();
            SetTextOfCurrentSuperCurrency();
            SetTextOfCurrentCurrencyPerSec();
            
            ListenedTypes.Add(typeof(ModifyCurrencyCommand));
            ListenedTypes.Add(typeof(ModifySuperCurrencyCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);        
        }

        public static InfVal GetCurrentCurrency()
        {
            return currentCurrency;
        }
        
        public static InfVal GetCurrentSuperCurrency()
        {
            return currentSuperCurrency;
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
        
        private void SetCurrentCurrencyPerSec(InfVal amount)
        {
            currentCurrencyPerSec = amount;
            SetTextOfCurrentSuperCurrency();
        }
        
        private void SetTextOfCurrentCurrency()
        {
            currentCurrencyText.text = currentCurrency.ToString(InGameData.MaxDigitsInInfVal);
        }
        
        private void SetTextOfCurrentSuperCurrency()
        {
            currentSuperCurrencyText.text = currentSuperCurrency.ToString(InGameData.MaxDigitsInInfVal);
        }
        
        private void SetTextOfCurrentCurrencyPerSec()
        {
            currentCurrencyPerSecText.text = $"{currentCurrencyPerSec.ToString(InGameData.MaxDigitsInInfVal)}/s";
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
                case SetCurrentCurrencyPerSecCommand command:
                {
                    SetCurrentCurrencyPerSec(command.Amount);
                    break;
                }

            }
        }
    }
}
