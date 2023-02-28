using System.Collections.Generic;
using System.Linq;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.Granary
{
    public class GranaryController : WorkplaceController
    {
        public void AddNewFieldForTractors(FarmFieldController farmFieldController)
        {
            List<TractorController> tractorControllers = workerControllers.OfType<TractorController>().ToList();
            if (tractorControllers.Count == 0) return;
            
            foreach (var tractor in tractorControllers)
            {
                tractor.AddNewField(farmFieldController);
            }
        }

        protected override void InitializeWorker()
        {
            base.InitializeWorker();
            
            
        }

        

    }
}