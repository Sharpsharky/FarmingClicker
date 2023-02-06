﻿using System;

namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;
    [Serializable]
    public record DisplayUpgradePanelCommand : Command
    {
        public UpgradeDisplayPopupData data;
        
        public DisplayUpgradePanelCommand(UpgradeDisplayPopupData data)
        {
            this.data = data;
        }
    }
}