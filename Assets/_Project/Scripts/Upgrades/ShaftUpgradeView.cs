using System;
using IdleGame.Growth;
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

        [SerializeField] private GrowthCalculator minerCountIncreaseGC;
        [SerializeField] private GrowthCalculator walkingSpeedIncreaseGC;
        [SerializeField] private GrowthCalculator resourcePerMineIncreaseGC;

        [SerializeField] private GrowthCalculator upgradeCostGC;

        [SerializeField] private UpgradeRow minersRow;
        [SerializeField] private UpgradeRow walkingSpeedRow;
        [SerializeField] private UpgradeRow resourcePerMine;

        [SerializeField] private TMP_Text shaftLevelText;
        [SerializeField] private TMP_Text upgradeCostText;
        [SerializeField] private Button upgradeButton;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip upgradeClip;

        private PlayerResource _playerResource;
        private int _currentLevel = 0;

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

            _currentLevel++;

            minerCountStat.SetCurrentValue(minerCountIncreaseGC.GetValue(_currentLevel));
            walkingSpeedStat.SetCurrentValue(walkingSpeedIncreaseGC.GetValue(_currentLevel));
            resourcePerMineStat.SetCurrentValue(resourcePerMineIncreaseGC.GetValue(_currentLevel));
            
            _playerResource.ReduceResourcesBy(upgradeCostGC.GetValue(_currentLevel));

            audioSource.PlayOneShot(upgradeClip);
            UpdateView();
        }

        public void UpdateView()
        {
            var minerCount = Mathf.RoundToInt(minerCountStat.currentValue);
            
            minersRow.UpdateView(minerCount.ToString(), minerCountIncreaseGC.GetValue(_currentLevel + 1).ToString("F0"));
            walkingSpeedRow.UpdateView(walkingSpeedStat.currentValue.ToString("F1"), walkingSpeedIncreaseGC.GetValue(_currentLevel + 1).ToString("F1"));
            resourcePerMine.UpdateView(resourcePerMineStat.currentValue.ToString("F1"), (resourcePerMineIncreaseGC).GetValue(_currentLevel + 1).ToString("F1"));
            
            upgradeCostText.text = upgradeCostGC.GetValue(_currentLevel).ToString("F0");
            upgradeButton.interactable = CanUpgrade();
            
            shaftLevelText.text = $"Shaft Level: {_currentLevel + 1}";
        }

        private bool CanUpgrade()
        {
            return _playerResource.CurrentResources >= upgradeCostGC.GetValue(_currentLevel);
        }
    }
}