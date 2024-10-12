using UnityEngine;

namespace IdleGame
{
    [CreateAssetMenu(fileName = "Stat", menuName = "IdleGame/New Stat", order = 0)]
    public class Stat : ScriptableObject
    {
        public float defaultValue;
        public float currentValue;
    }
}