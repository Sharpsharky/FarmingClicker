namespace FarmingClicker.GameFlow.Messages.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interactions;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public record StartInteractionCommand : Command
    {
        [SerializeField]
        [ValueDropdown(nameof(GetAvailableInteractions))]
        public string InteractionName;

        public List<string> DataToPass = new List<string>();
        
        #region Editor
        private List<string> GetAvailableInteractions()
        {
            return Enum.GetNames(typeof(TypesOfInteractions)).ToList();
        }
        #endregion
    }
}