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

        private FarmCalculationData initialFarmCalculationData;
        private SpriteRenderer spriteCombine;
        private const float OFFSET_OF_RIGHT_POINT_OF_WAY = 0.2f;
        
        
        public void Initialize(FarmFieldController farmFieldController, FarmCalculationData initialFarmCalculationData, 
            float leftEdgeOfCombineWay)
        {
            this.initialFarmCalculationData = initialFarmCalculationData;
            spriteCombine = GetComponentInChildren<SpriteRenderer>();
            transform.position = new Vector3(leftEdgeOfCombineWay, farmFieldController.transform.position.y,
                farmFieldController.transform.position.z);
            combineMovement.Initialize(farmFieldController, initialFarmCalculationData, leftEdgeOfCombineWay, GetRightEdgeOfWay(), spriteCombine.gameObject);
            combineAcquireCrops.Initialize(farmFieldController, combineMovement);
            
        }

        private float GetRightEdgeOfWay()
        {
            return initialFarmCalculationData.RightEdgePosition.x +
                   spriteCombine.size.x / 2 + OFFSET_OF_RIGHT_POINT_OF_WAY;
        }
    }
}