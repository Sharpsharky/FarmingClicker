namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using UnityEngine;
    public class TractorController : MonoBehaviour
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;

        [SerializeField] private float xOfLeftTractorPathRelativeToGranary = -0.5f;
        
        public void Initialize(Vector3 startingPoint, float yOfFirstStop, float distanceBetweenStops, int numberOfStops, float yOfGarage, Vector3 posOfGranary)
        {
            float xOfLeftTractorPath = posOfGranary.x + xOfLeftTractorPathRelativeToGranary;
            tractorMovement.Initialize(startingPoint,yOfFirstStop,distanceBetweenStops,numberOfStops,yOfGarage, posOfGranary.x,xOfLeftTractorPath);
        }
    }
}