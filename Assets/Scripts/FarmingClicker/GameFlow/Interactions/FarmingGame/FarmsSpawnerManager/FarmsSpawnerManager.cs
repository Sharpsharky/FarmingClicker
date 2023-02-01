using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Granary;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using FarmFields;

    public class FarmsSpawnerManager : SerializedMonoBehaviour, IMessageReceiver
    {
        private List<FarmFieldController> farmFieldControllers;
        private GranaryController granaryController;
        private FarmShopController farmShopController;
        
        [SerializeField] private GameObject granaryBuilding;
        [SerializeField] private GameObject shoppingBuilding;
        
        [SerializeField] private GameObject farmPathGameObject;
        [SerializeField] private GameObject farmPathFillerGameObject;
        
        [SerializeField] private GameObject farmFieldGameObject;
        [SerializeField] private GameObject farmFieldFillerGameObject;
        
        [SerializeField] private GameObject futureFarmFieldGameObject;
        
        private Vector3 positionOfGranaryBuilding;
        private Vector3 positionOfShoppingBuilding;
        
        private Vector3 positionOfFirstFarmPath;
        private Vector3 positionOfFirstFarmPathFiller;
        private Vector3 positionOfFirstFarmFieldFiller;
        private Vector3 positionOfFirstFarmField;
        private float xOfFirstUpgradeFarmFieldButton;
        
        private float xPosOfLeftEdgeOfScreen = 0;
        private SpriteRenderer spriteRendererOfFarmPathGameObject;
        private SpriteRenderer spriteRendererFillerGameObject;
        
        private Vector3 leftEdgePosition;
        private Vector3 rightEdgePosition;

        private int numberOfFarms = 5;
        
        public void Awake()
        {
            Debug.Log("Registering Navigation Manager.");

            MessageDispatcher.Instance.RegisterReceiver(this);
        }

        public FarmCalculationData Initialize()
        {
            InitializeVariables();
            GenerateBuildings();
            GenerateNumberOfFarms(numberOfFarms);

            return SetUpFarmData();

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
            
            xOfFirstUpgradeFarmFieldButton = CalculateFinalPosForUpgradeFarmFieldButton();
            
        }

        private FarmCalculationData SetUpFarmData()
        {
            FarmCalculationData farmCalculationData = new FarmCalculationData(positionOfGranaryBuilding, positionOfFirstFarmPath.y,
                spriteRendererFillerGameObject.bounds.size.y + spriteRendererOfFarmPathGameObject.bounds.size.y, 
                numberOfFarms, - 10, farmFieldControllers, granaryController, farmShopController, xOfFirstUpgradeFarmFieldButton);
            
            return farmCalculationData;
        }
        
        private void GenerateBuildings()
        {
            GameObject granaryBuildingGameObject = Instantiate(granaryBuilding, positionOfGranaryBuilding, Quaternion.identity);
            GameObject shoppingBuildingGameObject = Instantiate(shoppingBuilding, positionOfShoppingBuilding, Quaternion.identity);

            granaryController = granaryBuildingGameObject.GetComponent<GranaryController>();
            farmShopController = shoppingBuildingGameObject.GetComponent<FarmShopController>();
        }
        
        private void GenerateNumberOfFarms(int n)
        {
            for (int i = 0; i < n; i++)
            {
                GenerateFarmGameObject(i);
            }

            GenerateFutureFarmFieldGameObject(n);

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

        private float CalculateFinalPosForUpgradeFarmFieldButton()
        {
            return rightEdgePosition.x - 0.5f;
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
            GameObject newFarmFieldGameObject = Instantiate(farmFieldGameObject, positionOfCurrentFarmField, Quaternion.identity);
            
            Instantiate(farmPathFillerGameObject, positionOfCurrentFarmPathFiller, Quaternion.identity);
            Instantiate(farmFieldFillerGameObject, positionOfCurrentFarmFieldFiller, Quaternion.identity);

            
            farmFieldControllers.Add(newFarmFieldGameObject.GetComponent<FarmFieldController>());
            
        }

        private void GenerateFutureFarmFieldGameObject(int farmNumberPosition)
        {
            Vector3 positionOfFutureFarmField = positionOfFirstFarmField;
            positionOfFutureFarmField.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * farmNumberPosition + spriteRendererFillerGameObject.bounds.size.y * farmNumberPosition);
            
            Instantiate(futureFarmFieldGameObject, positionOfFutureFarmField, Quaternion.identity);
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
