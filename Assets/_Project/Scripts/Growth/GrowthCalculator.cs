using System.Collections.Generic;
using UnityEngine;

namespace IdleGame.Growth
{
    [CreateAssetMenu(menuName = "IdleGame/Growth/GrowthCalculator")]
    public class GrowthCalculator : ScriptableObject
    {
        public float startingValue = 1000;
        public int count = 100;
        [Range(0f, 1f)] public float growthRate = 0.1f;
        public bool floorValues = false;
        public List<float> growthValues = new List<float>();

        [ContextMenu("Calculate")]
        public List<float> CalculateGrowthValues()
        {
            growthValues = new List<float>();
            for (var i = 0; i < count; i++)
            {
                var value = startingValue * Mathf.Pow(1 + growthRate, i);
                if(floorValues) value = Mathf.Floor(value);
                growthValues.Add(value);
            }

            return growthValues;
        }

    }
}