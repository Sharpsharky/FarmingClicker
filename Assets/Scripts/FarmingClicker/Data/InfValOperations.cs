using InfiniteValue;

namespace FarmingClicker.Data
{
    public static class InfValOperations
    {
        public static string DisplayInfVal(InfVal val)
        {
            return val.ToPrecision(InGameData.InfValPrecisionDisplayed, true).ToString(InGameData.MaxDigitsInInfVal);
        } 
    }
}