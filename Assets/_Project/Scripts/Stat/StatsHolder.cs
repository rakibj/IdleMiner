using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame
{
    [DefaultExecutionOrder(-100)]
    public class StatsHolder : MonoBehaviour
    {
        [SerializeField] private List<Stat> allStats = new List<Stat>();

        private void Awake()
        {
            foreach (var stat in allStats)
            {
                if (PlayerPrefs.HasKey(stat.name)) stat.currentValue = PlayerPrefs.GetFloat(stat.name);
                else stat.currentValue = stat.defaultValue;
            }
        }

        private void OnEnable()
        {
            foreach (var stat in allStats)
            {
                stat.OnUpdated += OnStatUpdated;
            }
        }
        private void OnDisable()
        {
            foreach (var stat in allStats)
            {
                stat.OnUpdated -= OnStatUpdated;
            }
        }

        private void OnStatUpdated(Stat stat)
        {
            PlayerPrefs.SetFloat(stat.name, stat.currentValue);
        }
    }
}