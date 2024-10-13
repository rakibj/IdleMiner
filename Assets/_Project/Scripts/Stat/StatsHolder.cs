using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame
{
    public class StatsHolder : MonoBehaviour
    {
        [SerializeField] private List<Stat> allStats = new List<Stat>();

        private void Awake()
        {
            foreach (var stat in allStats) stat.currentValue = stat.defaultValue;
        }
    }
}