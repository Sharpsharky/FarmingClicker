using System.Collections;
using Unity.VisualScripting;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using UnityEngine;
    public class TractorMovement : MonoBehaviour
    {

        [SerializeField] private float speed = 1f;

        private int direction = -1;
        private bool isStopped = true;
        private float nextStopY;
        private float currentStopCount = 0;
        private float xOfRightTractorPath;
        private float xOfLeftTractorPath;
        
        private Vector3 startingPoint;
        private float yOfFirstStop;
        private float distanceBetweenStops;
        private int numberOfStops;
        private float yOfGarage;
        private bool isGoingToTheLastStop = false;
        
        public void Initialize(Vector3 startingPoint, float yOfFirstStop, float distanceBetweenStops, int numberOfStops, 
            float yOfGarage, float xOfRightTractorPath, float xOfLeftTractorPath)
        {
            this.startingPoint = startingPoint;
            this.yOfFirstStop = yOfFirstStop;
            this.distanceBetweenStops = distanceBetweenStops;
            this.numberOfStops = numberOfStops;
            this.yOfGarage = yOfGarage;
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
            if (direction < 0 && transform.position.y <= nextStopY) StopForLoading();
            else if (direction > 0 && transform.position.y >= nextStopY) StopForLoading();


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

            if (currentStopCount >= numberOfStops)
            {
                nextStopY = yOfGarage;
                isGoingToTheLastStop = true;
            }
            else
            {
                nextStopY = yOfFirstStop - distanceBetweenStops * currentStopCount;
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
            }
            else
            {
                var currentPos = transform.position;
                currentPos.x = xOfRightTractorPath;
                transform.position = currentPos;
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