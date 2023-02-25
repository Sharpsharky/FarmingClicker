namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using System;
    using System.Collections.Generic;
    using InfiniteValue;
    using UnityEngine;

    public class ManageTractorSprites : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tractorSpriteRenderer;
        [SerializeField] private List<Sprite> tractorsDown;
        [SerializeField] private List<Sprite> tractorsUp;

        private TractorDirections currentDirection = TractorDirections.DOWN;
        private int currentCropsLoadIndex = 0;


        public void ChangeDirection(TractorDirections newDirection)
        {
            currentDirection = newDirection;
            if (currentDirection == TractorDirections.UP)
                tractorSpriteRenderer.sprite = tractorsUp[currentCropsLoadIndex];
            else
                tractorSpriteRenderer.sprite = tractorsDown[currentCropsLoadIndex];

        }

        public void ChangeCropLoad(InfVal currentLoad, InfVal maxLoad)
        {
            InfVal loadSpace = maxLoad / tractorsDown.Count;

            string loadIndexString = (currentLoad / loadSpace).ToString();
            int loadIndex = Int32.Parse(loadIndexString);

            if (loadIndex >= tractorsDown.Count) loadIndex = tractorsDown.Count - 1;

            currentCropsLoadIndex = loadIndex;

            ChangeDirection(currentDirection);

        }
        
        
        
    }
    
    public enum TractorDirections {DOWN, UP}
    
    
}


