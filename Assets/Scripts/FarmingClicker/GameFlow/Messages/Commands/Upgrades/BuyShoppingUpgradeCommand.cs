namespace FarmingClicker.GameFlow.Messages.Commands.Upgrades
{
    using System;

    [Serializable]
    public record BuyShoppingUpgradeCommand : Command
    {
        public int AmountOfBoughtUpgrades;
        
        public BuyShoppingUpgradeCommand(int amountOfBoughtUpgrades)
        {
            AmountOfBoughtUpgrades = amountOfBoughtUpgrades;
        }
    }

}