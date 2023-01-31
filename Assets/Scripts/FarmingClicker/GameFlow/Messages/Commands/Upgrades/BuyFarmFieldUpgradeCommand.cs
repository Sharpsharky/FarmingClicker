namespace FarmingClicker.GameFlow.Messages.Commands.Upgrades
{
    using System;

    [Serializable]
    public record BuyFarmFieldUpgradeCommand : Command
    {
        public int FarmIndex;
        public int AmountOfBoughtUpgrades;
        
        public BuyFarmFieldUpgradeCommand(int farmIndex, int amountOfBoughtUpgrades)
        {
            FarmIndex = farmIndex;
            AmountOfBoughtUpgrades = amountOfBoughtUpgrades;
        }
    }

}