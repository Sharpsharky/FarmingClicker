using System;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;

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
        
        public event Action<GranaryController> OnLorryStoppedInGranary;
        public event Action OnLorryStoppedInShop;
        
        public void Initialize(GranaryController granaryController, FarmShopController farmShopController
            , float movementSpeed, InfVal maxLoad)
        {
            this.granaryController = granaryController;
            this.farmShopController = farmShopController;
            currentMovementSpeed = movementSpeed;
            currentMaxLoad = maxLoad;
            ModifyRotation();
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
            NotifyOnStop();
            ReverseDirection();
            StartCoroutine(LoadingTime(1));
        }

        private void NotifyOnStop()
        {
            if (currentDir < 0) OnLorryStoppedInGranary(granaryController);
            else OnLorryStoppedInShop();
        }
        
        private void ReverseDirection()
        {
            currentDir *= -1;
            ModifyRotation();
        }

        private void ModifyRotation()
        {
            Quaternion curRot = transform.rotation;

            if (currentDir < 0)
            {
                curRot.y = 180;
            }
            else
            {
                curRot.y = 0;
            }

            transform.rotation = curRot;
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