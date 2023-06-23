namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Combine
{
    using System;
    using System.Collections;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Workplaces.FarmFields;
    using FarmsSpawnerManager;

    public class CombineMovement : SerializedMonoBehaviour
    {
        private FarmFieldController farmFieldController;
        
        private bool isStopped;
        private int currentDir = -1;
        
        public event Action OnCombineStoppedOnChest;
        public event Action OnCombineStartMoving;
        public event Action OnCombineStoppedMoving;
        private FarmCalculationData initialFarmCalculationData;

        private float xPosOfRightMaxPos;
        private float xPosOfLeftMaxPos;

        private void Start()
        {
            isStopped = true;
        }

        public void Initialize(FarmFieldController farmFieldController, FarmCalculationData initialFarmCalculationData)
        {
            this.farmFieldController = farmFieldController;
            this.initialFarmCalculationData = initialFarmCalculationData;
            xPosOfRightMaxPos = initialFarmCalculationData.RightEdgePosition.x;
            xPosOfLeftMaxPos = farmFieldController.transform.position.x - farmFieldController.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            ModifyRotation();
            isStopped = true;
        }
        
        private void Update()
        {
            if (isStopped) return;
            
            transform.position += new Vector3(currentDir,0,0) * farmFieldController.WorkerProperties.MovingSpeed
                                                              * Time.deltaTime;
            
            if (currentDir < 0 && transform.position.x <= xPosOfRightMaxPos)
            {
                Debug.Log($"direction: {currentDir}, transform.position.x: {transform.position.x}");
                StopForTurningBack();
            }
            else if 
                (currentDir > 0 && transform.position.x >= xPosOfLeftMaxPos) StopForTurningBack();


        }
     
        private void NotifyOnStop()
        {
            if (currentDir < 0) StopForTurningBack();
            else OnCombineStoppedOnChest();
        }
        
        private void StopForTurningBack()
        {
            NotifyOnStop();
            ReverseDirection();
            StartCoroutine(TurningBackTime(1));
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
        
        
        private IEnumerator TurningBackTime(float loadingTime)
        {
            isStopped = true;
            OnCombineStoppedMoving();
            
            yield return new WaitForSeconds(loadingTime);

            isStopped = false;
            OnCombineStartMoving();

            yield return null;
        }
    }
}