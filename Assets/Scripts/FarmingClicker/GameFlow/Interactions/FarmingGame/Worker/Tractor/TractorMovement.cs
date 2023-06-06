namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Workplaces;
    using Workplaces.FarmFields;
    using UnityEngine;
    using Core.Message;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Granary;
    using UnityEngine.Serialization;

    public class TractorMovement : MonoBehaviour
    {

        [FormerlySerializedAs("speed")] [SerializeField] private float movingSpeed = 1f;

        private int direction = -1;
        private bool isStopped = true;
        private float nextStopY;
        private int currentStopCount = 0;
        private float xOfRightTractorPath;
        private float xOfLeftTractorPath;
        
        private Vector3 startingPoint;
        private float distanceBetweenStops;
        private float yOfGarage;
        private bool isGoingToTheLastStop = false;
        private List<FarmFieldController> farmFieldControllers;
        private TractorController tractorController;

        public event Action<FarmFieldController> OnTractorStoppedOnFarmField;
        
        public void Initialize(TractorController tractorController, List<FarmFieldController> workplaceControllers,
            Vector3 startingPoint, float distanceBetweenStops, float xOfRightTractorPath, float xOfLeftTractorPath)
        {
            this.tractorController = tractorController;
            farmFieldControllers = new List<FarmFieldController>(workplaceControllers);


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
            
            transform.position += new Vector3(0,direction,0) * 
                                  tractorController.WorkplaceController.WorkerProperties.MovingSpeed * Time.deltaTime;
            
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
            if (currentStopCount > 0 && currentStopCount <= farmFieldControllers.Count)
            {
                StoppedByFarmField(currentStopCount-1);
            }
            
            if (isGoingToTheLastStop) //If going to the last stop (Garage)
            {
                ChangeDirection();
                isGoingToTheLastStop = false;
                nextStopY = startingPoint.y;
                currentStopCount = 0;
                return;
            }

            if (direction > 0) //If going to Granary
            {
                StoppedInGranary();
            }

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
        private void StoppedByFarmField(int indexOfFarmField)
        {
            Debug.Log($"Start Collecting Crops!");
            OnTractorStoppedOnFarmField(farmFieldControllers[indexOfFarmField]);
        }
        
        private void StoppedInGranary()
        {
            Debug.Log($"StoppedInGranary!");
            
            ChangeDirection();
            
            var data = new TractorWentToGranaryNotification(tractorController);
            MessageDispatcher.Instance.Send(data);
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