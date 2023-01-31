namespace FarmingClicker.GameFlow.Messages.Commands.Upgrades
{
    using System;

    [Serializable]
    public record BuyGranaryUpgradeCommand : Command
    {
        public int AmountOfBoughtUpgrades;
        
        public BuyGranaryUpgradeCommand(int amountOfBoughtUpgrades)
        {
            AmountOfBoughtUpgrades = amountOfBoughtUpgrades;
        }
    }

}