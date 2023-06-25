using FarmingClicker.Data;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Combine
{
    using Workplaces.FarmFields;
    using UnityEngine;
    using FarmsSpawnerManager;

    public class CombineController : WorkerController
    {
        [SerializeField] private CombineMovement combineMovement;
        [SerializeField] private CombineAcquireCrops combineAcquireCrops;

        public void Initialize(FarmFieldController farmFieldController, FarmCalculationData initialFarmCalculationData)
        {
            var rotationSpriteGameObject = GetComponentInChildren<SpriteRenderer>().gameObject;
            transform.position = new Vector3(UniversalProperties.LeftPointOfCombineWayX, farmFieldController.transform.position.y,
                farmFieldController.transform.position.z);
            combineMovement.Initialize(farmFieldController, initialFarmCalculationData, UniversalProperties.LeftPointOfCombineWayX, 
                UniversalProperties.RightPointOfCombineWayX, rotationSpriteGameObject);
            combineAcquireCrops.Initialize(farmFieldController, combineMovement);
            
        }

    }
}