namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Combine
{
    using Workplaces;
    using Workplaces.FarmFields;
    using UnityEngine;
    using FarmsSpawnerManager;

    public class CombineController : WorkerController
    {
        [SerializeField] private CombineMovement combineMovement;
        [SerializeField] private CombineAcquireCrops combineAcquireCrops;

        public void Initialize(FarmFieldController farmFieldController, FarmCalculationData initialFarmCalculationData, 
            float leftEdgeOfCombineWay)
        {
            transform.position = new Vector3(leftEdgeOfCombineWay, farmFieldController.transform.position.y,
                farmFieldController.transform.position.z);
            combineMovement.Initialize(farmFieldController, initialFarmCalculationData, leftEdgeOfCombineWay);
            combineAcquireCrops.Initialize(farmFieldController, combineMovement);
            
        }
    }
}