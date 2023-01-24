using System;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    public class TractorController : MonoBehaviour
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;

        private void Awake()
        {
            
        }
    }
}