namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    using Worker;

    public class WorkerManagerStatistics
    {
        public int LevelOfManager = 0;
        public StatisticsTypes StatisticsType;
        public int FaceImageId;
        public bool isEmpty = true;

        public WorkerManagerStatistics(int levelOfManager, StatisticsTypes statisticsType, int faceImageId)
        {
            LevelOfManager = levelOfManager;
            StatisticsType = statisticsType;
            FaceImageId = faceImageId;
            isEmpty = false;
        }

        public WorkerManagerStatistics()
        {
        }
    }
}