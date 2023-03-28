namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Workplaces.FarmShop;
    using System.Collections;
    using InfiniteValue;
    using Workplaces.Granary;
    public class LorryMovement : SerializedMonoBehaviour
    {
        private float currentMovementSpeed = 1;
        private InfVal currentMaxLoad = 1;

        private GranaryController granaryController;
        private FarmShopController farmShopController;
        
        private bool isStopped;
        private int currentDir = -1;
        
        public void Initialize(GranaryController granaryController, FarmShopController farmShopController
            , float movementSpeed, InfVal maxLoad)
        {
            this.granaryController = granaryController;
            this.farmShopController = farmShopController;
            currentMovementSpeed = movementSpeed;
            currentMaxLoad = maxLoad;
        
        }
        
        private void Update()
        {
            if (isStopped) return;
            
            transform.position += new Vector3(currentDir,0,0) * currentMovementSpeed * Time.deltaTime;
            
            if (currentDir < 0 && transform.position.x <= granaryController.transform.position.x)
            {
                Debug.Log($"direction: {currentDir}, transform.position.x: {transform.position.x}");
                StopForLoading();
            }
            else if 
                (currentDir > 0 && transform.position.x >= farmShopController.transform.position.x) StopForLoading();


        }
        
        private void StopForLoading()
        {
            ReverseDirection();
            StartCoroutine(LoadingTime(1));
        }

        private void ReverseDirection()
        {
            currentDir *= -1;
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