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
        
        private Vector3 startingPoint;
        private float yOfFirstStop;
        private float distanceBetweenStops;
        private int numberOfStops;
        private float yOfGarage;
        private bool isGoingToTheLastStop = false;
        public void Initialize(Vector3 startingPoint, float yOfFirstStop, float distanceBetweenStops, int numberOfStops, float yOfGarage)
        {
            this.startingPoint = startingPoint;
            this.yOfFirstStop = yOfFirstStop;
            this.distanceBetweenStops = distanceBetweenStops;
            this.numberOfStops = numberOfStops;
            this.yOfGarage = yOfGarage;

            transform.position = startingPoint;
            IterateStop();

            isStopped = false;
        }
        
        private void Update()
        {
            if (isStopped) return;
            
            transform.position += new Vector3(0,direction,0) * speed * Time.deltaTime;
            if (transform.position.y < nextStopY) StopForLoading();


        }

        private void StopForLoading()
        {
            StartCoroutine(LoadingTime(1));
            IterateStop();
        }

        private void IterateStop()
        {
            if (currentStopCount >= numberOfStops)
            {
                nextStopY = yOfGarage;
                isGoingToTheLastStop = true;
            }
            else
            {
                if (isGoingToTheLastStop) ChangeDirection();
                nextStopY = yOfFirstStop - distanceBetweenStops * currentStopCount;
            }

            currentStopCount++;

        }
        

        private void ChangeDirection()
        {
            direction *= -1;
            isGoingToTheLastStop = false;
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