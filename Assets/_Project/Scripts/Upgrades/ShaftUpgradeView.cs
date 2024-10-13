using UnityEngine;

namespace IdleGame.Upgrades
{
    public class ShaftUpgradeView : MonoBehaviour
    {
        [SerializeField] private Stat minerCountStat;
        [SerializeField] private Stat walkingSpeedStat;
        [SerializeField] private Stat resourcePerMineStat;
        [SerializeField] private Stat minerWorkDuration;


        [ContextMenu("Upgrade")]
        public void Upgrade()
        {
            minerCountStat.SetCurrentValue(minerCountStat.currentValue + 1);
            walkingSpeedStat.SetCurrentValue(walkingSpeedStat.currentValue + 0.1f);
            resourcePerMineStat.SetCurrentValue(resourcePerMineStat.currentValue + 1);
            
            LogUpgrades();
        }

        public void LogUpgrades()
        {
            var totalExtraction = minerCountStat.currentValue * resourcePerMineStat.currentValue;
            var minerCount = Mathf.RoundToInt(minerCountStat.currentValue);
            var walkingSpeed = walkingSpeedStat.currentValue;
            var miningSpeed = resourcePerMineStat.currentValue/ minerWorkDuration.currentValue;
            var workerCapacity = resourcePerMineStat.currentValue * minerWorkDuration.currentValue;

            Debug.Log("totalExtraction " + totalExtraction);
            Debug.Log("minerCount " + minerCount);
            Debug.Log("walkingSpeed " + walkingSpeed);
            Debug.Log("miningSpeed " + miningSpeed);
            Debug.Log("workerCapacity " + workerCapacity);

        }
    }
}