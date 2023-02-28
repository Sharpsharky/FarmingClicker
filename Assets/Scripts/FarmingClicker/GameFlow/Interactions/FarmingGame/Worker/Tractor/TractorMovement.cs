
using System.Collections;
using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;
using UnityEngine;
using System.Linq;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{

    public class TractorMovement : MonoBehaviour, IWorkerMovable
    {

        [SerializeField] private float speed = 1f;

        private int direction = -1;
        private bool isStopped = true;
        private float nextStopY;
        private float currentStopCount = 0;
        private float xOfRightTractorPath;
        private float xOfLeftTractorPath;
        
        private Vector3 startingPoint;
        private float distanceBetweenStops;
        private float yOfGarage;
        private bool isGoingToTheLastStop = false;
        private List<FarmFieldController> farmFieldControllers;
        private TractorController tractorController;
        
        public void Initialize(WorkerController tractorController, List<WorkplaceController> workplaceControllers, Vector3 startingPoint,
            float distanceBetweenStops, float xOfRightTractorPath, float xOfLeftTractorPath)
        {
            
            if(tractorController is TractorController _tractorController)
                this.tractorController = _tractorController;

            if (workplaceControllers is List<WorkplaceController> workplaceControllerList)
            {
                this.farmFieldControllers = workplaceControllerList.OfType<FarmFieldController>().ToList();
            }
            
            this.startingPoint = startingPoint;
            this.distanceBetweenStops = distanceBetweenStops;
            SetNewYOfGarage();
            this.xOfRightTractorPath = xOfRightTractorPath;
            this.xOfLeftTractorPath = xOfLeftTractorPath;
            
            transform.position = startingPoint;
            IterateStop();

            isStopped = false;         
        }
        
        
        private void Update()
        {
            if (isStopped) return;
            
            transform.position += new Vector3(0,direction,0) * speed * Time.deltaTime;
            if (direction < 0 && transform.position.y <= nextStopY)
            {
                Debug.Log($"direction: {direction}, transform.position.y: {transform.position.y}, nextStopY: {nextStopY}");
                StopForLoading();
            }
            else if (direction > 0 && transform.position.y >= nextStopY) StopForLoading();


        }
        
        public void AddNewField(WorkplaceController farmFieldController)
        {
            if (farmFieldController is not FarmFieldController _farmFieldController) return;
            farmFieldControllers.Add(_farmFieldController);
            SetNewYOfGarage();
            
            if (isGoingToTheLastStop)
            {
                isGoingToTheLastStop = false;
                currentStopCount = farmFieldControllers.Count - 1;
                IterateStop();
            }

            if (direction > 0 && isStopped)
            {
                var currentPos = transform.position;
                currentPos.y = yOfGarage;
                transform.position = currentPos;
            }

            
        }

        private void SetNewYOfGarage()
        {
            yOfGarage = farmFieldControllers[^1].gameObject.transform.position.y - distanceBetweenStops;
        }
        
        private void StopForLoading()
        {
            StartCoroutine(LoadingTime(1));
            IterateStop();
        }

        private void IterateStop()
        {
            if (isGoingToTheLastStop) //If going to the last stop (Garage)
            {
                ChangeDirection();
                isGoingToTheLastStop = false;
                nextStopY = startingPoint.y;
                currentStopCount = 0;
                return;
            }

            if (direction > 0) ChangeDirection(); //If going to Granary

            if (currentStopCount >= farmFieldControllers.Count)
            {
                nextStopY = yOfGarage;
                isGoingToTheLastStop = true;
            }
            else
            {
                nextStopY = farmFieldControllers[0].gameObject.transform.position.y - distanceBetweenStops * currentStopCount;
            }

            currentStopCount++;

        }
        

        private void ChangeDirection()
        {
            direction *= -1;
            if (direction > 0)
            {
                var currentPos = transform.position;
                currentPos.x = xOfLeftTractorPath;
                transform.position = currentPos;            
                tractorController.ChangeDirection(TractorDirections.UP);

            }
            else
            {
                var currentPos = transform.position;
                currentPos.x = xOfRightTractorPath;
                transform.position = currentPos;
                tractorController.ChangeDirection(TractorDirections.DOWN);
            }


        }

        private IEnumerator LoadingTime(float loadingTime)
        {
            isStopped = true;

            yield return new WaitForSeconds(loadingTime);

            isStopped = false;
            
            yield return null;
        }

        
    }
}