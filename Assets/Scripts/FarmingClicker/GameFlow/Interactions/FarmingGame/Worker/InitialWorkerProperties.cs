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
        public InfVal MaxTransportedCurrency = new InfVal(20).ToPrecision(9);
        public float WorkingSpeed = 1;
        public float MovingSpeed = 1;
        public InfVal CostOfNextLevel = new InfVal(10).ToPrecision(9);
        public InfVal CroppedCurrency = new InfVal(0).ToPrecision(9);
    }
}