using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.FutureFarmField;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmShop;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.Granary;
using FarmingClicker.GameFlow.Messages.Commands.NewField;
using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class FarmsSpawnerManager : SerializedMonoBehaviour, IMessageReceiver
    {
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();
        private List<GranaryController> granaryControllers = new List<GranaryController>();
        private List<FarmShopController> farmShopControllers = new List<FarmShopController>();
        private FutureFarmFieldController futureFarmFieldController;
        
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

        private int numberOfFarms;
        public List<Type> ListenedTypes { get; } = new List<Type>();

        public FarmCalculationData Initialize(int numberOfFarms)
        {
            this.numberOfFarms = numberOfFarms;
            
            InitializeVariables();
            GenerateBuildings();
            GenerateNumberOfFarms(numberOfFarms);

            ListenedTypes.Add(typeof(BuyNewFieldCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
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
                numberOfFarms, futureFarmFieldController.gameObject.transform.position.y, farmFieldControllers, 
                granaryControllers, farmShopControllers, futureFarmFieldController, xOfFirstUpgradeFarmFieldButton);
            
            return farmCalculationData;
        }
        
        private void GenerateBuildings()
        {
            GameObject granaryBuildingGameObject = Instantiate(granaryBuilding, positionOfGranaryBuilding, Quaternion.identity);
            GameObject shoppingBuildingGameObject = Instantiate(shoppingBuilding, positionOfShoppingBuilding, Quaternion.identity);


            var granaryController = granaryBuildingGameObject.GetComponent<GranaryController>();
            granaryControllers.Add(granaryController);
            LoadDataFarmManager.instance.FarmGranaryControllers.Add(granaryController);

            var farmShopController = shoppingBuildingGameObject.GetComponent<FarmShopController>();
            farmShopControllers.Add(farmShopController);
            LoadDataFarmManager.instance.FarmShopControllers.Add(farmShopController);

        }
        
        private void GenerateNumberOfFarms(int n)
        {
            for (int i = 0; i < n; i++)
            {
                GenerateFarmComponents(i);
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

        
        private FarmFieldController GenerateFarmComponents(int nOfFarm)
        {
            Debug.Log("Generate Farm Components");

            ConstructNewFarmPath(nOfFarm);
            FarmFieldController newFarmFieldController = ConstructNewFarmField(nOfFarm);
            ConstructNewFarmFieldFiller(nOfFarm);
            ConstructNewFarmPathFiller(nOfFarm);
            LoadDataFarmManager.instance.FarmFieldControllers.Add(newFarmFieldController);

            return newFarmFieldController;
        }

        private FarmFieldController ConstructNewFarmField(int nOfFarm)
        {
            Vector3 positionOfCurrentFarmField = positionOfFirstFarmField;
            positionOfCurrentFarmField.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            GameObject newFarmFieldGameObject = Instantiate(farmFieldGameObject, positionOfCurrentFarmField, Quaternion.identity);
            FarmFieldController newFarmFieldController = newFarmFieldGameObject.GetComponent<FarmFieldController>();
            farmFieldControllers.Add(newFarmFieldController);
            return newFarmFieldController;
        }
        private void ConstructNewFarmPath(int nOfFarm)
        {
            Vector3 positionOfCurrentFarmPath = positionOfFirstFarmPath;
            positionOfCurrentFarmPath.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            Instantiate(farmPathGameObject, positionOfCurrentFarmPath, Quaternion.identity);
        }
        private void ConstructNewFarmFieldFiller(int nOfFarm)
        {
            Vector3 positionOfCurrentFarmPathFiller = positionOfFirstFarmPathFiller;
            positionOfCurrentFarmPathFiller.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            Instantiate(farmPathFillerGameObject, positionOfCurrentFarmPathFiller, Quaternion.identity);

        }
        private void ConstructNewFarmPathFiller(int nOfFarm)
        {
            Vector3 positionOfCurrentFarmFieldFiller = positionOfFirstFarmFieldFiller;
            positionOfCurrentFarmFieldFiller.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * nOfFarm + spriteRendererFillerGameObject.bounds.size.y * nOfFarm);
            Instantiate(farmFieldFillerGameObject, positionOfCurrentFarmFieldFiller, Quaternion.identity);
        }
        
        
        private void GenerateFutureFarmFieldGameObject(int farmNumberPosition)
        {
            Vector3 positionOfFutureFarmField = positionOfFirstFarmField;
            positionOfFutureFarmField.y -= (spriteRendererOfFarmPathGameObject.bounds.size.y * farmNumberPosition + spriteRendererFillerGameObject.bounds.size.y * farmNumberPosition);
            
            var futureFarmField= Instantiate(futureFarmFieldGameObject, positionOfFutureFarmField, Quaternion.identity);
            futureFarmFieldController = futureFarmField.GetComponent<FutureFarmFieldController>();
        }
        
        
        public void OnMessageReceived(object message)
        {
            if (!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"Navigation manager received load scene command.");

            switch (message)
            {
                case BuyNewFieldCommand buyNewFieldCommand:
                {
                    FarmFieldController newFarmFieldController = GenerateFarmComponents(farmFieldControllers.Count);
                    MessageDispatcher.Instance.Send(new FarmFieldConstructedNotification(newFarmFieldController));
                    break;
                }
                
            }
        }
    }
}
