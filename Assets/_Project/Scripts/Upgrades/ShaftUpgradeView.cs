using System;
using UnityEngine;

namespace IdleGame.Upgrades
{
    public class ShaftUpgradeView : MonoBehaviour
    {
        [SerializeField] private Stat minerCountStat;
        [SerializeField] private Stat walkingSpeedStat;
        [SerializeField] private Stat resourcePerMineStat;

        [SerializeField] private int minerCountIncrease = 1;
        [SerializeField] private float walkingSpeedIncrease = 0.1f;
        [SerializeField] private float resourcePerMineIncreaseBy = 1;

        [SerializeField] private UpgradeRow minersRow;
        [SerializeField] private UpgradeRow walkingSpeedRow;
        [SerializeField] private UpgradeRow resourcePerMine;

        private void Start()
        {
            UpdateView();
        }

        [ContextMenu("Upgrade")]
        public void Upgrade()
        {
            minerCountStat.SetCurrentValue(minerCountStat.currentValue + minerCountIncrease);
            walkingSpeedStat.SetCurrentValue(walkingSpeedStat.currentValue + walkingSpeedIncrease);
            resourcePerMineStat.SetCurrentValue(resourcePerMineStat.currentValue + resourcePerMineIncreaseBy);
            
            UpdateView();
        }

        public void UpdateView()
        {
            var minerCount = Mathf.RoundToInt(minerCountStat.currentValue);
            
            minersRow.Update(minerCount.ToString(), minerCountIncrease.ToString());
            walkingSpeedRow.Update(walkingSpeedStat.currentValue.ToString(), walkingSpeedIncrease.ToString());
            resourcePerMine.Update(resourcePerMineStat.currentValue.ToString(), (resourcePerMineIncreaseBy).ToString());
        }

    }
}