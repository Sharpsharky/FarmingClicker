using System.Collections.Generic;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.ScalingParametersSO
{
    [CreateAssetMenu(fileName = "New Scaling Statistic", menuName = "FarmingClicker/Scaling/ScalingStatisticInt")]
    public class ScalingParametersInt : ScriptableObject
    {
        [SerializeField] private List<ScalingParameterInt> levelsOfNewAmounts = new List<ScalingParameterInt>();
    }
}

