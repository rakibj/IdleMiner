using System;
using IdleGame.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private float upgradeCost = 20;

        [SerializeField] private UpgradeRow minersRow;
        [SerializeField] private UpgradeRow walkingSpeedRow;
        [SerializeField] private UpgradeRow resourcePerMine;

        [SerializeField] private TMP_Text upgradeCostText;
        [SerializeField] private Button upgradeButton;
        
        private PlayerResource _playerResource;

        private void Awake()
        {
            _playerResource = FindObjectOfType<PlayerResource>();
        }

        private void OnEnable()
        {
            _playerResource.OnResourceUpdated += OnPlayerResourceUpdated;
            upgradeButton.onClick.AddListener(Upgrade);
        }

        private void OnDisable()
        {
            _playerResource.OnResourceUpdated -= OnPlayerResourceUpdated;
            upgradeButton.onClick.RemoveListener(Upgrade);
        }

        private void OnPlayerResourceUpdated()
        {
            UpdateView();
        }

        private void Start()
        {
            UpdateView();
        }

        [ContextMenu("Upgrade")]
        public void Upgrade()
        {
            if (!CanUpgrade()) return;
            
            minerCountStat.SetCurrentValue(minerCountStat.currentValue + minerCountIncrease);
            walkingSpeedStat.SetCurrentValue(walkingSpeedStat.currentValue + walkingSpeedIncrease);
            resourcePerMineStat.SetCurrentValue(resourcePerMineStat.currentValue + resourcePerMineIncreaseBy);
            
            _playerResource.ReduceResourcesBy(upgradeCost);
            
            UpdateView();
        }

        public void UpdateView()
        {
            var minerCount = Mathf.RoundToInt(minerCountStat.currentValue);
            
            minersRow.Update(minerCount.ToString(), minerCountIncrease.ToString());
            walkingSpeedRow.Update(walkingSpeedStat.currentValue.ToString(), walkingSpeedIncrease.ToString());
            resourcePerMine.Update(resourcePerMineStat.currentValue.ToString(), (resourcePerMineIncreaseBy).ToString());
            
            upgradeCostText.text = upgradeCost.ToString();
            upgradeButton.interactable = CanUpgrade();
        }

        private bool CanUpgrade()
        {
            return _playerResource.CurrentResources >= upgradeCost;
        }
    }
}