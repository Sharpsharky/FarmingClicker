namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    using Worker;
    using UnityEngine;

    public class WorkerManagerStatistics
    {
        public int LevelOfManager = 0;
        public StatisticsTypes StatisticsType;
        public int FaceImageId;


        public WorkerManagerStatistics(int levelOfManager, StatisticsTypes statisticsType, int faceImageId)
        {
            LevelOfManager = levelOfManager;
            StatisticsType = statisticsType;
            FaceImageId = faceImageId;
        }
    }
}