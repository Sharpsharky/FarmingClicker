using System.Collections.Generic;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    public class ManageTractorSprites : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tractorSpriteRenderer;
        [SerializeField] private List<Sprite> tractorsDown;
        [SerializeField] private List<Sprite> tractorsUp;

        
        public void ChangeSprite(TractorDirections tractorDirections, int tractorLoadLevel)
        {
            List<Sprite> tractorList;
            if (tractorDirections == TractorDirections.UP) tractorList = tractorsUp;
            else tractorList = tractorsDown;

            tractorSpriteRenderer.sprite = tractorList[tractorLoadLevel];

        }
    }
    
    public enum TractorDirections {DOWN, UP}
    
    
}


