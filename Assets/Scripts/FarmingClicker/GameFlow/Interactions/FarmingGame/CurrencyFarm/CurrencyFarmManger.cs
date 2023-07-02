namespace FarmingClicker.GameFlow.Interactions.FarmingGame.CurrencyFarm
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Messages.Commands.Currency;
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class CurrencyFarmManger : SerializedMonoBehaviour, IMessageReceiver
    {
        private static InfVal currentCurrency = 0;
        private static InfVal currentSuperCurrency = 0;
        private static InfVal currentCurrencyPerSec = 0;
        
        private void Awake()
        {
            currentCurrency = 0;
            currentSuperCurrency = 0;
            currentCurrencyPerSec = 0;

            MessageDispatcher.Instance.Send(new SetTextOfCurrentCurrencyCommand(currentCurrency));
            MessageDispatcher.Instance.Send(new SetTextOfCurrentSuperCurrencyCommand(currentSuperCurrency));
            MessageDispatcher.Instance.Send(new SetTextOfCurrentCurrencyPerSecCommand(currentCurrencyPerSec));
            
            ListenedTypes.Add(typeof(ModifyCurrencyCommand));
            ListenedTypes.Add(typeof(ModifySuperCurrencyCommand));
            ListenedTypes.Add(typeof(SetCurrentCurrencyPerSecCommand));
            ListenedTypes.Add(typeof(AddCoinValueFromRewardCommand));
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
            MessageDispatcher.Instance.Send(new SetTextOfCurrentCurrencyCommand(currentCurrency));

        }
        
        private void ModifyCurrentSuperCurrency(InfVal amountToAdd)
        {
            currentSuperCurrency += amountToAdd;
            MessageDispatcher.Instance.Send(new SetTextOfCurrentSuperCurrencyCommand(currentSuperCurrency));

        }
        
        private void SetCurrentCurrencyPerSec(InfVal amount)
        {
            Debug.Log("currentCurrencyPerSec:" + amount);
            currentCurrencyPerSec = amount;
            MessageDispatcher.Instance.Send(new SetTextOfCurrentCurrencyPerSecCommand(currentCurrencyPerSec));

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
                case AddCoinValueFromRewardCommand command:
                {
                    ModifyCurrentCurrency(command.Amount);
                    break;
                }
            }
        }
    }
}
