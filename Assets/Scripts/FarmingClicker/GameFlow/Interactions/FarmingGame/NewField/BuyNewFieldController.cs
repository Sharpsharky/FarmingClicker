namespace FarmingClicker.GameFlow.Interactions.FarmingGame.NewField
{
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmingClicker.Data.Popup;
    using FarmingClicker.GameFlow.Messages.Commands.NewField;
    using InfiniteValue;
    using Data;
    using CurrencyFarm;
    using Messages.Commands.Currency;
    public class BuyNewFieldController : PopupPanelBase, IMessageReceiver
    {
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button buyButton;
        
        [SerializeField, BoxGroup("Statistics")] private TMP_Text priceText;
        [SerializeField, BoxGroup("Statistics")] private TMP_Text timeOfConstructionText;
        
        [SerializeField, BoxGroup("Titles")] private TMP_Text priceTitle;
        [SerializeField, BoxGroup("Titles")] private TMP_Text timeOfConstructionTitle;
        [SerializeField, BoxGroup("Titles")] private TMP_Text titleText;
        
        [SerializeField, BoxGroup("I2Loc")] private string priceTitleI2Loc;
        [SerializeField, BoxGroup("I2Loc")] private string timeOfConstructionTitleI2Loc;
        [SerializeField, BoxGroup("I2Loc")] private string titleTextI2Loc;
        
        private void Awake()
        {
            priceTitle.text = $"{I2.Loc.LocalizationManager.GetTranslation(priceTitleI2Loc)}:";
            timeOfConstructionTitle.text = $"{I2.Loc.LocalizationManager.GetTranslation(timeOfConstructionTitleI2Loc)}:";
            titleText.text = $"{I2.Loc.LocalizationManager.GetTranslation(titleTextI2Loc)}:";
        }

        public override void SetupData(IPopupData data)
        {
            if (data is not BuyNewFieldPopupData buyNewFieldPopupData) return;

            priceText.text = buyNewFieldPopupData.Price.ToPrecision(InGameData.InfValPrecisionDisplayed).ToString(InGameData.MaxDigitsInInfVal);
            timeOfConstructionText.text = buyNewFieldPopupData.TimeOfConstruction.ToString();
            
            buyButton.onClick.AddListener(()=>{Buy(buyNewFieldPopupData.Price, buyNewFieldPopupData.TimeOfConstruction);});
            exitButton.onClick.AddListener(ExitPanel);
            
            ListenedTypes.Add(typeof(UpdateBuyNewFieldPanelCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
            gameObject.SetActive(true);
        }

        private void Buy(InfVal price, int timeOfConstruction)
        {
            Debug.Log("Buy farm");
            if (price > CurrencyFarmManger.GetCurrentCurrency()) return;
            
            MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(-price));
            
            MessageDispatcher.Instance.Send(new BuyNewFieldCommand(price, timeOfConstruction));
            ExitPanel();
        }

        private void ExitPanel()
        {
            buyButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            
            gameObject.SetActive(false);
        }

        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case UpdateBuyNewFieldPanelCommand updateBuyNewFieldPanel:
                {
                    priceText.text = updateBuyNewFieldPanel.Price.ToString();
                    timeOfConstructionText.text = updateBuyNewFieldPanel.TimeOfConstruction.ToString();
                    
                    buyButton.onClick.RemoveAllListeners();
                    buyButton.onClick.AddListener(()=>{Buy(updateBuyNewFieldPanel.Price, updateBuyNewFieldPanel.TimeOfConstruction);});
                    break;
                }
                
            }
            
        }
    }
}
