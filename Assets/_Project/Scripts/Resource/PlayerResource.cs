using System;
using TMPro;
using UnityEngine;

namespace IdleGame.Resource
{
    public class PlayerResource : MonoBehaviour
    {
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private TMP_Text resourceText;
        private float _currentResources;
        private const string PlayerResourceKey = "PlayerResourceKey";
        public float CurrentResources
        {
            get
            {
                if(PlayerPrefs.HasKey(PlayerResourceKey)) _currentResources = PlayerPrefs.GetFloat(PlayerResourceKey, _currentResources);
                return _currentResources;
            }
            set
            {
                _currentResources = value;
                resourceText.text = Mathf.CeilToInt(_currentResources).ToString();
                PlayerPrefs.SetFloat(PlayerResourceKey, _currentResources);
                OnResourceUpdated?.Invoke();
            }
        }

        public Action OnResourceUpdated;

        private void Start()
        {
            CurrentResources = CurrentResources;
        }

        private void OnEnable()
        {
            resourceContainer.OnResourcesReceived += AddResources;
        }

        private void OnDisable()
        {
            resourceContainer.OnResourcesReceived -= AddResources;
        }

        private void AddResources(float amount)
        {
            CurrentResources += amount;
        }

        public void ReduceResourcesBy(float amount)
        {
            CurrentResources -= amount;
        }
        
    }
}