using System;
using Core.Message;
using Core.Message.Interfaces;
using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Managers;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    using System.Collections.Generic;
    using FarmingClicker.Data.Popup;
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using GlobalData;
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class SelectManagerController : PopupPanelBase, IMessageReceiver
    {
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Managers")] private List<WorkerManagerFaceController> workerManagers = 
            new List<WorkerManagerFaceController>();
        
        public List<Type> ListenedTypes { get; } = new List<Type>();

        public override void SetupData(IPopupData data)
        {
            if (data is not SelectManagerPopupData selectManagerPopupData) return;

            InitializeManagers(selectManagerPopupData.WorkerManagerStatistics);
            exitButton.onClick.AddListener(Exit);
            
            gameObject.SetActive(true);
            
            ListenedTypes.Add(typeof(WorkerManagerSelectedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);        
        }

        private void InitializeManagers(List<WorkerManagerStatistics> workerManagerStatistics)
        {
            if (workerManagers.Count < workerManagerStatistics.Count)
            {
                Debug.LogError($"Too many workerManager statistics ({workerManagerStatistics.Count}) for " +
                               $"workerManagers in UI ({workerManagers.Count})");
                return;
            }

            for (int i = 0; i < workerManagerStatistics.Count; i++)
            {
                workerManagers[i].Initialize(workerManagerStatistics[i]);

                workerManagers[i].FaceButton.onClick.AddListener(() =>
                {
                    ClickManagerButton(workerManagerStatistics[i]);
                });
                
                workerManagers[i].gameObject.SetActive(true);
            }
        }

        private void ClickManagerButton(WorkerManagerStatistics workerManagerStatistics)
        {
            MessageDispatcher.Instance.Send(new WorkerManagerSelectedNotification(workerManagerStatistics));
        }
        
        
        
        private void Exit()
        {
            gameObject.SetActive(false);

            foreach (var manager in workerManagers)
            {
                manager.gameObject.SetActive(false);
            }
            
            exitButton.onClick.RemoveAllListeners();
        }

        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case WorkerManagerSelectedNotification workerManagerSelectedNotification:
                {
                    MessageDispatcher.Instance.Send(new NewWorkerManagerSelectedNotification
                        (workerManagerSelectedNotification.WorkerManagerStatistics));
                    Exit();
                    break;
                }
                
            }
            
        }
    }
}