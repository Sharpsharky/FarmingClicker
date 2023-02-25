using InfiniteValue;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.ScalingParametersSO
{
    [CreateAssetMenu(fileName = "New Scaling Statistic", menuName = "FarmingClicker/Scaling/ScalingStatisticInfVal")]
    public class ScalingParametersInfVal : ScriptableObject
    {
        [SerializeField] private List<ScalingParameterInfVal> levelsOfNewAmounts = new List<ScalingParameterInfVal>();
    }
}

