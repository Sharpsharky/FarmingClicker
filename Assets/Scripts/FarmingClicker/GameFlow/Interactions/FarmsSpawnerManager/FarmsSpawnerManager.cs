using System;
using System.Collections.Generic;
using Core.Message;
using Core.Message.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmsSpawnerManager
{
    public class FarmsSpawnerManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField, InlineEditor] private GameObject farmGameObject;
        [SerializeField, InlineEditor] private Vector3 positionOfFirstFarm;

        private int currentFarmsCount = 0;
        
        public void Awake()
        {
            Debug.Log("Registering Navigation Manager.");

            MessageDispatcher.Instance.RegisterReceiver(this);
        }

        private void GenerateFarmGameObject()
        {
            currentFarmsCount++;
            
            
            
        }
        
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            
            if (!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"Navigation manager received load scene command.");

            //switch (message)
            //{

                //case LoadSceneCommand loadSceneCommand:
                //{
                //    break;
                //}
                
            //}
        }
    }
}
