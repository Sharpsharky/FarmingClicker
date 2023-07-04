namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class FarmFieldCurrentlyBuildingData
    {
        public int SecondsToBeDone = 0;
        public bool IsBeingConstructed = false;

        public FarmFieldCurrentlyBuildingData(int secondsToBeDone, bool isBeingConstructed)
        {
            SecondsToBeDone = secondsToBeDone;
            IsBeingConstructed = isBeingConstructed;
        }
    }
}