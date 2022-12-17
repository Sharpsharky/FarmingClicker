namespace Core.Message
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class MessageDispatcher : MonoBehaviour, IMessageDispatcher
    {
        public static MessageDispatcher Instance;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        
        #region Private Variables

        [ShowInInspector] private Dictionary<Type, List<IMessageReceiver>> receivers = new();

        #endregion

        #region Public Methods

        public void RegisterReceiver(IMessageReceiver receiver)
        {
            foreach(var listenedType in receiver.ListenedTypes)
            {
                if(receivers.TryGetValue(listenedType, out var existingItem))
                {
                    // no duplicates
                    if(existingItem.Contains(receiver))
                    {
                        continue;
                    }

                    existingItem.Add(receiver);
                    continue;
                }

                receivers.Add(listenedType, new List<IMessageReceiver> {receiver});
            }
        }

        public void UnregisterReceiver(IMessageReceiver receiver)
        {
            var entriesToRemove = new List<Type>();
            foreach(var listenedType in receiver.ListenedTypes)
            {
                if(!receivers.TryGetValue(listenedType, out var existingItem))
                {
                    continue;
                }

                existingItem.Remove(receiver);
                if(existingItem.Count == 0)
                {
                    entriesToRemove.Add(listenedType);
                }
            }

            // remove empty dictionary entries
            for(int index = 0; index < entriesToRemove.Count; index++)
            {
                var entryToRemove = entriesToRemove[index];
                receivers.Remove(entryToRemove);
            }
        }

        public void Send<T>(T message)
        {
            if(!receivers.TryGetValue(message.GetType(), out var receiversList))
            {
                return;
            }

            for(int index = 0; index < receiversList.Count; index++)
            {
                var receiver = receiversList[index];
                receiver.OnMessageReceived(message);
            }
        }

        #endregion
    }
}