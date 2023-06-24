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
        private GameObject spriteCombineGo;

        private bool isStopped;
        private int currentDir = -1;
        
        public event Action OnCombineStoppedOnChest;
        public event Action OnCombineStartMoving;
        public event Action OnCombineStoppedMoving;
        private FarmCalculationData initialFarmCalculationData;

        private float xPosOfRightMaxPos;
        private float xPosOfLeftMaxPos;

        private GameObject spriteOfCombine;


        public void Initialize(FarmFieldController farmFieldController, FarmCalculationData initialFarmCalculationData, 
            float leftEdgeOfCombineWay, float rightEdgeOfCombineWay, GameObject spriteCombineGo)
        {
            this.spriteCombineGo = spriteCombineGo;
            this.farmFieldController = farmFieldController;
            this.initialFarmCalculationData = initialFarmCalculationData;
            xPosOfLeftMaxPos = leftEdgeOfCombineWay;
            xPosOfRightMaxPos = rightEdgeOfCombineWay;
            Debug.Log($"xPosOfLeftMaxPos: {xPosOfLeftMaxPos}, xPosOfRightMaxPos: {xPosOfRightMaxPos}");
            //ModifyRotation();
            isStopped = true;
            StartCoroutine(StartTheCombineWithRandomDelay());
        }

        private IEnumerator StartTheCombineWithRandomDelay()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.8f));
            
            isStopped = false;
            OnCombineStartMoving();

            yield return null;
        }
        
        
        private void Update()
        {
            if (isStopped) return;
            
            transform.position += new Vector3(-currentDir,0,0) * farmFieldController.WorkerProperties.MovingSpeed
                                                              * Time.deltaTime;
            
            if (currentDir > 0 && transform.position.x <= xPosOfLeftMaxPos)
            {
                StopForTurningBack();
            }
            else if (currentDir < 0 && transform.position.x >= xPosOfRightMaxPos)
            {

                StopForTurningBack();
            }


        }
        private void StopForTurningBack()
        {

            if (currentDir > 0) OnCombineStoppedOnChest();
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

            Quaternion curRot = spriteCombineGo.transform.rotation;

            if (currentDir > 0)
            {
                curRot.y = 180;
            }
            else
            {
                curRot.y = 0;
            }

            spriteCombineGo.transform.rotation = curRot;
        }
        
        
        private IEnumerator TurningBackTime(float loadingTime)
        {
            isStopped = true;
            OnCombineStoppedMoving();
            Debug.Log($"dupa1");
            yield return new WaitForSeconds(loadingTime);

            isStopped = false;
            OnCombineStartMoving();
            Debug.Log($"dupa2");

            yield return null;
        }
    }
}