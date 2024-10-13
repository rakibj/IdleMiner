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

        public float CurrentResources
        {
            get => _currentResources;
            set
            {
                _currentResources = value;
                resourceText.text = Mathf.CeilToInt(_currentResources).ToString();
                OnResourceUpdated?.Invoke();
            }
        }

        public Action OnResourceUpdated;

        private void Start()
        {
            CurrentResources = 0;
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