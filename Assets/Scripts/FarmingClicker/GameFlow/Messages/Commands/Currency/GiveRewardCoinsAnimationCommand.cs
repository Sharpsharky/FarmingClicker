namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record GiveRewardCoinsAnimationCommand : Command
    {
        public InfVal Amount;

        public GiveRewardCoinsAnimationCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}