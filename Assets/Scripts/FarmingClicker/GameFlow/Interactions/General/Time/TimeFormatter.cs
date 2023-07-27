namespace FarmingClicker.GameFlow.Interactions.General.Time
{
    public static class TimeFormatter
    {
        public static string FormatTimeToMMSS(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;

            return $"{minutes:00}:{remainingSeconds:00}";
        }
    }
}