using System.Collections;
using Unity.VisualScripting;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using UnityEngine;
    public class TractorMovement : MonoBehaviour
    {

        [SerializeField] private float speed = 1f;

        private int direction = -1;
        private bool isStopped = false;
        private float nextStopY;
        private float currentStopCount = 0;
        
        private Vector3 startingPoint;
        private float yOfFirstStop;
        private float distanceBetweenStops;
        private int numberOfStops;
        private float yOfGarage;

        private void Initialize(Vector3 startingPoint, float yOfFirstStop, float distanceBetweenStops, int numberOfStops, float yOfGarage)
        {
            this.startingPoint = startingPoint;
            this.yOfFirstStop = yOfFirstStop;
            this.distanceBetweenStops = distanceBetweenStops;
            this.numberOfStops = numberOfStops;
            this.yOfGarage = yOfGarage;

            transform.position = startingPoint;
            IterateStop();

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
            if (currentStopCount >= 3) nextStopY = yOfGarage;
            else
            {
                nextStopY = yOfFirstStop - distanceBetweenStops * currentStopCount;
            }

            currentStopCount++;

            if (currentStopCount >= 3)
            {
                nextStopY = yOfGarage;
                ChangeDirection();
            }
        }
        

        private void ChangeDirection()
        {
            direction *= -1;
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