using System;
using Core.Message;
using FarmingClicker.Data.Popup;
using FarmingClicker.Dialogue.DialogueDataTypes;
using FarmingClicker.Dialogue.DialoguePanelControllers;
using FarmingClicker.GameFlow.Messages.Commands.Popups;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers.RandomManager
{
    public class RandomManagersController : PopupPanelBase
    {
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button randomButtonBuy;
        [SerializeField, BoxGroup("Buttons")] private Button randomButtonWatchVideo;
        
        public override void SetupData(IPopupData data)
        {
            if (data is not RandomManagersPopupData randomManagersPopupData) return;
            
            exitButton.onClick.AddListener(Exit);
            randomButtonBuy.onClick.AddListener(() =>
            {
                DrawNewManager(randomManagersPopupData.DrawNewRandomManagers);
            });
            randomButtonWatchVideo.onClick.AddListener(() =>
            {
                DrawNewManager(randomManagersPopupData.DrawNewRandomManagers);
            });
            
            gameObject.SetActive(true);
        }

        private void DrawNewManager(Action DrawNewRandomManagers)
        {
            DrawNewRandomManagers?.Invoke();
            Exit();
        }
        
        
        private void Exit()
        {
            exitButton.onClick.RemoveAllListeners();
            randomButtonBuy.onClick.RemoveAllListeners();
            randomButtonWatchVideo.onClick.RemoveAllListeners();
            
            gameObject.SetActive(false);
        }
        
    }
}