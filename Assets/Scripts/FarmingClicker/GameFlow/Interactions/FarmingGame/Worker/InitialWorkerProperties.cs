namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Initial Worker Properties", menuName = "Initial Properties")]
    public class InitialWorkerProperties : SerializedScriptableObject
    {
        public int UpgradeLevel = 1;
        public int NumberOfWorkers = 1;
        public InfVal MaxTransportedCurrency = 20;
        public float WorkingSpeed = 1;
        public float MovingSpeed = 1;
        public InfVal CostOfNextLevel = 10;
        public InfVal CroppedCurrency = 0;
    }
}