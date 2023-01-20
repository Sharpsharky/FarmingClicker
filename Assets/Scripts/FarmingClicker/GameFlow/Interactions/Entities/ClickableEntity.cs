namespace FarmingClicker.GameFlow.Interactions.Entities
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Messages;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.EventSystems;
    
    [Serializable]
    public class ClickableEntity : SerializedMonoBehaviour, IPointerClickHandler
    {
        [SerializeField] public List<Command> commandList = new List<Command>();
            
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("clicked!");
            foreach(var command in commandList)
            {
                MessageDispatcher.Instance.Send(command);
            }
        }
    }
}
