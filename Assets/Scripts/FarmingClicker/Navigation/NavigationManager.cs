namespace FarmingClicker.Navigation
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using GameFlow.Messages;
    using GameFlow.Messages.Commands;
    using GameFlow.Messages.Notifications.Scene;
    using UnityEngine;
    using Utils.ScenesHelper;

    public class NavigationManager : MonoBehaviour, IMessageReceiver
    {
        
        public List<Type> ListenedTypes { get; } = new List<Type>();

        private string currentScene = "";
        
        public void Initialize()
        {
            Debug.Log("Registering Navigation Manager.");
            ListenedTypes.Add(typeof(LoadInteractionSceneCommand));
            ListenedTypes.Add(typeof(LoadSceneCommand));
            ListenedTypes.Add(typeof(SceneLoadedNotification));
            ListenedTypes.Add(typeof(SceneUnloadedNotification));
            ListenedTypes.Add(typeof(StartInteractionCommand));
            ListenedTypes.Add(typeof(ExitInteractionCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
        }

        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;
            
            Debug.Log($"Navigation manager received load scene command.");

            switch(message)
            {                

                case LoadSceneCommand loadSceneCommand:
                {
                    string unloadString = loadSceneCommand.Unload ? "unloading" : "loading";
                    Debug.Log($"Load scene command executed, {unloadString} {loadSceneCommand.SceneName}.");
                    if(loadSceneCommand.Unload)
                    {
                        SceneMaintainer.UnloadScene(loadSceneCommand.SceneName, () =>
                        {
                            var notification = new SceneUnloadedNotification(loadSceneCommand.SceneName);
                            MessageDispatcher.Instance.Send(notification);
                        });
                        return;
                    }
                    
                    SceneMaintainer.LoadScene(loadSceneCommand.SceneName, list =>
                    {
                        Debug.Log(loadSceneCommand.DataToPass);
                        var notification = new SceneLoadedNotification(loadSceneCommand.SceneName, loadSceneCommand.DataToPass);
                            MessageDispatcher.Instance.Send(notification);
                    });
                    return;
                }
                case SceneLoadedNotification sceneLoadedNotification:
                {
                    Debug.Log($"Loaded scene {sceneLoadedNotification.SceneName}.");
                    
                    var notification = new SceneDataPassCommand(sceneLoadedNotification.DataToPass);
                    MessageDispatcher.Instance.Send(notification);
                    
                    return;
                }
                case SceneUnloadedNotification sceneUnloadedNotification:
                {
                    Debug.Log($"Unloaded scene {sceneUnloadedNotification.SceneName}.");
                    return;
                }

                default:
                {
                
                    Debug.Log($"Navigation manager received message that it couldn't process.");
                    break;
                }
            }
        }

        
    }
}