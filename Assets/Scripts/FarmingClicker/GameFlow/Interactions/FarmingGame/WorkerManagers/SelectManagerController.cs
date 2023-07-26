using FarmingClicker.GameFlow.Messages.Commands.Managers;
using FarmingClicker.GameFlow.Messages.Commands.Popups;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    using System.Collections.Generic;
    using FarmingClicker.Data.Popup;
    using Dialogue.DialogueDataTypes;
    using Dialogue.DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using System;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Managers;
    using UnityEngine.UI;
    
    public class SelectManagerController : PopupPanelBase, IMessageReceiver
    {
        [SerializeField, BoxGroup("Buttons")] private Button exitButton;
        [SerializeField, BoxGroup("Buttons")] private Button randomManagersButton;
        
        [SerializeField, BoxGroup("Managers")] private List<WorkerManagerFaceController> workerManagers = 
            new List<WorkerManagerFaceController>();

        private List<WorkerManagerStatistics> workerManagerStatistics = new List<WorkerManagerStatistics>();
        
        public List<Type> ListenedTypes { get; } = new List<Type>();

        public override void SetupData(IPopupData data)
        {
            if (data is not SelectManagerPopupData selectManagerPopupData) return;

            InitializeManagers(selectManagerPopupData.WorkerManagerStatistics);
            exitButton.onClick.AddListener(Exit);

            randomManagersButton.onClick.AddListener(() =>
            {
                ClickRandomManagersButton(selectManagerPopupData.DrawNewRandomManagers);
            });
            
            gameObject.SetActive(true);
            
            ListenedTypes.Add(typeof(WorkerManagerSelectedNotification));
            ListenedTypes.Add(typeof(NewWorkerManagerSelectedNotification));
            ListenedTypes.Add(typeof(ReloadManagersCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);        
        }

        private void InitializeManagers(List<WorkerManagerStatistics> workerManagerStats)
        {
            workerManagerStatistics = new List<WorkerManagerStatistics>(workerManagerStats);

            if (workerManagers.Count < workerManagerStatistics.Count)
            {
                Debug.LogError($"Too many workerManager statistics ({workerManagerStatistics.Count}) for " +
                               $"workerManagers in UI ({workerManagers.Count})");
                return;
            }

            for (int i = 0; i < workerManagerStatistics.Count; i++)
            {
                var currentWorkerManagerStatistics = workerManagerStatistics[i];
                workerManagers[i].Initialize(currentWorkerManagerStatistics);
                
                workerManagers[i].FaceButton.onClick.AddListener(() =>
                {
                    ClickManagerButton(currentWorkerManagerStatistics);
                });
                
                workerManagers[i].gameObject.SetActive(true);
            }
        }

        private void ClickManagerButton(WorkerManagerStatistics workerManagerStatistics)
        {
            MessageDispatcher.Instance.Send(new NewWorkerManagerSelectedNotification(workerManagerStatistics));
            Exit();
        }

        private void ClickRandomManagersButton(Action drawNewRandomManagers)
        {
            MessageDispatcher.Instance.Send(new DisplayRandomManagersCommand(new RandomManagersPopupData(drawNewRandomManagers)));
        }
        
        private void Exit()
        {
            gameObject.SetActive(false);

            foreach (var manager in workerManagers)
            {
                manager.gameObject.SetActive(false);
            }
            
            exitButton.onClick.RemoveAllListeners();
            randomManagersButton.onClick.RemoveAllListeners();
        }

        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case WorkerManagerSelectedNotification workerManagerSelectedNotification:
                {
                    //MessageDispatcher.Instance.Send(new NewWorkerManagerSelectedNotification
                    //    (workerManagerSelectedNotification.WorkerManagerStatistics));
                    //Exit();
                    break;
                }
                case ReloadManagersCommand reloadManagersCommand:
                {
                    InitializeManagers(reloadManagersCommand.WorkerManagersReadyToSelect);
                    break;
                }
                
            }
            
        }
    }
}