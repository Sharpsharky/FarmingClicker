namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using UnityEngine;
    public class TractorController : MonoBehaviour
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;

        public void Initialize(Vector3 startingPoint, float yOfFirstStop, float distanceBetweenStops, int numberOfStops, float yOfGarage)
        {
            tractorMovement.Initialize(startingPoint,yOfFirstStop,distanceBetweenStops,numberOfStops,yOfGarage);
        }
    }
}