
namespace FarmingClicker.GameFlow.Interactions.FarmsSpawnerManager
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class FarmsSpawnerManager : SerializedMonoBehaviour, IMessageReceiver
    {
        
        [SerializeField, InlineEditor] private GameObject granaryBuilding;
        [SerializeField, InlineEditor] private GameObject shoppingBuilding;
        
        [SerializeField, InlineEditor] private GameObject farmPathGameObject;
        [SerializeField, InlineEditor] private GameObject farmPathFillerGameObject;
        
        [SerializeField, InlineEditor] private GameObject farmFieldGameObject;
        [SerializeField, InlineEditor] private GameObject farmFieldFillerGameObject;
        
        [SerializeField, InlineEditor] private GameObject upgradeFarmFieldButton;
        [SerializeField, InlineEditor] private GameObject upgradeGranaryBuildingButton;
        [SerializeField, InlineEditor] private GameObject upgradeShoppingBuildingButton;
        
        private Vector3 positionOfGranaryBuilding;
        private Vector3 positionOfShoppingBuilding;
        
        private Vector3 positionOfFirstFarmPath;
        private Vector3 positionOfFirstFarmPathFiller;
        private Vector3 positionOfFirstFarmFieldFiller;
        private Vector3 positionOfFirstFarmField;
        
        private float xPosOfLeftEdgeOfScreen = 0;
        private SpriteRenderer spriteRendererOfFarmPathGameObject;
        private SpriteRenderer spriteRendererFillerGameObject;
        
        private Vector3 leftEdgePosition;
        private Vector3 rightEdgePosition;
        
        public void Awake()
        {
            Debug.Log("Registering Navigation Manager.");

            MessageDispatcher.Instance.RegisterReceiver(this);
        }

        public void Initialize()
        {
            InitializeVariables();
            GenerateBuildings();
            GenerateNumberOfFarms(5);
        }
        

        private void InitializeVariables()
        {
            spriteRendererOfFarmPathGameObject = farmPathGameObject.GetComponent<SpriteRenderer>();
            spriteRendererFillerGameObject = farmPathFillerGameObject.GetComponent<SpriteRenderer>();
            leftEdgePosition = CalculateLeftEdgeOfScreen();
            rightEdgePosition = CalculateRightEdgeOfScreen();
            
            positionOfFirstFarmPath = CalculateFinalPosForTheFirstFarmPath();
            positionOfFirstFarmPathFiller = CalculateFinalPosForTheFirstFarmPathFiller();
            positionOfFirstFarmField = CalculateFinalPosForTheFirstFarmField();
            positionOfFirstFarmFieldFiller = CalculateFinalPosForTheFirstFarmFieldFiller();

            positionOfGranaryBuilding = CalculateFinalPosForTheGranaryBuilding();
            positionOfShoppingBuilding = CalculateFinalPosForTheShoppingBuilding();
        }

        private void GenerateBuildings()
        {
            Instantiate(granaryBuilding, positionOfGranaryBuilding, Quaternion.identity);
            Instantiate(upgradeGranaryBuildingButton, positionOfGranaryBuilding, Quaternion.identity);
            Instantiate(shoppingBuilding, positionOfShoppingBuilding, Quaternion.identity);
            Instantiate(upgradeShoppingBuildingButton, positionOfShoppingBuilding, Quaternion.identity);
        }
        
        private void GenerateNumberOfFarms(int n)
        {
            for (int i = 0; i < n; i++)
            {
                GenerateFarmGameObject(i);
            }
        }
        
        
        
        private Vector3 CalculateLeftEdgeOfScreen()
        {
            Vector3 leftEdgePos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
            return leftEdgePos;
        }
        
        private Vector3 CalculateRightEdgeOfScreen()
        {
            Vector3 rightEdgePos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));
            return rightEdgePos;
        }

        private Vector3 CalculateFinalPosForTheGranaryBuilding()
        {
            var spriteRendererGranaryBuilding = granaryBuilding.GetComponent<SpriteRenderer>();

            float posXGranaryBuilding = leftEdgePosition.x + (spriteRendererGranaryBuilding.bounds.size.x)/2;
            float posYGranaryBuilding = leftEdgePosition.y + (spriteRendererGranaryBuilding.bounds.size.y)/2;
            return new Vector3(posXGranaryBuilding,posYGranaryBuilding,0);
        }

        private Vector3 CalculateFinalPosForTheShoppingBuilding()
        {
            var spriteRendererShoppingBuilding = shoppingBuilding.GetComponent<SpriteRenderer>();

            float posXGranaryBuilding = rightEdgePosition.x - (spriteRendererShoppingBuilding.bounds.size.x)/2;
            float posYGranaryBuilding = rightEdgePosition.y + (spriteRendererShoppingBuilding.bounds.size.y)/2;
            return new Vector3(posXGranaryBuilding,posYGranaryBuilding,0);
        }
        
        
        private Vector3 CalculateFinalPosForTheFirstFarmPath()
        {
            float posXLeftEdgeWithHalfOfSpriteW = leftEdgePosition.x + (spriteRendererOfFarmPathGameObject.bounds.size.x)/2;
            float middleOfTheScreenY = leftEdgePosition.y;
            float posYMiddleWithHalfOfSpriteW = middleOfTheScreenY - (spriteRendererOfFarmPathGameObject.bounds.size.y/2);
            return new Vector3(posXLeftEdgeWithHalfOfSpriteW,posYMiddleWithHalfOfSpriteW,0);
        }
        
        private Vector3 CalculateFinalPosForTheFirstFarmField()
        {
            Vector3 finalPosOfFarmField = positionOfFirstFarmPath;
            
            float newX = finalPosOfFarmField.x + spriteRendererOfFarmPathGameObject.bounds.size.x/2 + 
                         farmFieldGameObject.GetComponent<SpriteRenderer>().bounds.size.x/2;
            finalPosOfFarmField.x = newX;
            return finalPosOfFarmField;
        }
        
        private Vector3 CalculateFinalPosForTheFirstFarmPathFiller()
        {
            Vector3 finalPosOfFirstFarmPathFiller = positionOfFirstFarmPath;
            
            float newY = finalPosOfFirstFarmPathFiller.y - (spriteRendererOfFarmPathGameObject.bounds.size.y/2 + 
                                                            spriteRendererFillerGameObject.bounds.size.y/2);
            finalPosOfFirstFarmPathFiller.y = newY;
            return finalPosOfFirstFarmPathFiller;
        }
        
        private Vector3 CalculateFinalPosForTheFirstFarmFieldFiller()
        {
            Vector3 finalPosOfFirstFarmFieldFiller = positionOfFirstFarmField;
            float newY = finalPosOfFirstFarmFieldFiller.y - (spriteRendererOfFarmPathGameObject.bounds.size.y/2 + 
                                                             spriteRendererFillerGameObject.bounds.size.y/2);
            finalPosOfFirstFarmFieldFiller.y = newY;
            return finalPosOfFirstFarmFieldFiller;
        }

        
        private void GenerateFarmGameObject(int nOfFarm)
        {
            Debug.Log("GenerateFarmGameObject");            
            
            Vector3 positionOfCurrentFarmPath = positionOfFirstFarmPath;
            positionOfCurrentFarmPath.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            
            Vector3 positionOfCurrentFarmField = positionOfFirstFarmField;
            positionOfCurrentFarmField.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            
            Vector3 positionOfCurrentFarmPathFiller = positionOfFirstFarmPathFiller;
            positionOfCurrentFarmPathFiller.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);

            Vector3 positionOfCurrentFarmFieldFiller = positionOfFirstFarmFieldFiller;
            positionOfCurrentFarmFieldFiller.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            
            
            Instantiate(farmPathGameObject, positionOfCurrentFarmPath, Quaternion.identity);
            Instantiate(farmFieldGameObject, positionOfCurrentFarmField, Quaternion.identity);
            Instantiate(upgradeFarmFieldButton, positionOfCurrentFarmField, Quaternion.identity);
            Instantiate(farmPathFillerGameObject, positionOfCurrentFarmPathFiller, Quaternion.identity);
            Instantiate(farmFieldFillerGameObject, positionOfCurrentFarmFieldFiller, Quaternion.identity);
            

        }
        
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public void OnMessageReceived(object message)
        {
            
            if (!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"Navigation manager received load scene command.");

            //switch (message)
            //{

                //case LoadSceneCommand loadSceneCommand:
                //{
                //    break;
                //}
                
            //}
        }
    }
}
