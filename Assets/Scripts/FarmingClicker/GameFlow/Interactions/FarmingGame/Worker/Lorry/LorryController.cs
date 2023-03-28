namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry
{
    using UnityEngine;

    public class LorryController : WorkerController
    {
        
        [SerializeField] private LorryMovement tractorMovement;

        
        public void Initialize()
        {
            tractorMovement.Initialize();
        }
    }
}