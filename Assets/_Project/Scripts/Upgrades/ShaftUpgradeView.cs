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
        [SerializeField] private Stat shaftLevelStat;

        private int CurrentLevel
        {
            set => shaftLevelStat.SetCurrentValue(value);
            get => (int) shaftLevelStat.currentValue;
        }

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

            CurrentLevel++;

            minerCountStat.SetCurrentValue(minerCountIncreaseGC.GetValue(CurrentLevel));
            walkingSpeedStat.SetCurrentValue(walkingSpeedIncreaseGC.GetValue(CurrentLevel));
            resourcePerMineStat.SetCurrentValue(resourcePerMineIncreaseGC.GetValue(CurrentLevel));
            
            _playerResource.ReduceResourcesBy(upgradeCostGC.GetValue(CurrentLevel));

            audioSource.PlayOneShot(upgradeClip);
            UpdateView();
        }

        public void UpdateView()
        {
            var minerCount = Mathf.RoundToInt(minerCountStat.currentValue);
            
            minersRow.UpdateView(minerCount.ToString(), minerCountIncreaseGC.GetValue(CurrentLevel + 1).ToString("F0"));
            walkingSpeedRow.UpdateView(walkingSpeedStat.currentValue.ToString("F1"), walkingSpeedIncreaseGC.GetValue(CurrentLevel + 1).ToString("F1"));
            resourcePerMine.UpdateView(resourcePerMineStat.currentValue.ToString("F1"), (resourcePerMineIncreaseGC).GetValue(CurrentLevel + 1).ToString("F1"));
            
            upgradeCostText.text = upgradeCostGC.GetValue(CurrentLevel).ToString("F0");
            upgradeButton.interactable = CanUpgrade();
            
            shaftLevelText.text = $"Shaft Level: {CurrentLevel + 1}";
        }

        private bool CanUpgrade()
        {
            return _playerResource.CurrentResources >= upgradeCostGC.GetValue(CurrentLevel);
        }
    }
}