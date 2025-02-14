using System;
using IdleGame.Command;
using TMPro;
using UnityEngine;

namespace IdleGame.Resource
{
    public class ResourceContainer : MonoBehaviour
    {
        [SerializeField] private Stat produceByStat;
        [SerializeField] private ResourceContainer transferFrom;
        [SerializeField] private ResourceContainer transferTo;
        [SerializeField] private CommandRunner commandRunner;
        [SerializeField] private CommandBehavior produceCommand;
        [SerializeField] private CommandBehavior transferFromCommand;
        [SerializeField] private CommandBehavior transferToCommand;
        [SerializeField] private float _currentResources = 0f;
        [SerializeField] private TMP_Text resourceAmountText;
        [SerializeField] private Stat capacityStat;
        public Action<float> OnResourcesReceived;

        public float CurrentResources
        {
            set
            {
                _currentResources = Mathf.Min(capacityStat.currentValue, value);
                resourceAmountText.text = _currentResources.ToString("F0");
            }
            get => _currentResources;
        }

        private void Start()
        {
            CurrentResources = 0;
        }

        private void OnEnable()
        {
            if(commandRunner) commandRunner.OnCommandCompleted += OnCommandCompleted;
        }

        private void OnDisable()
        {
            if(commandRunner) commandRunner.OnCommandCompleted -= OnCommandCompleted;
        }

        private void OnCommandCompleted(CommandBehavior command)
        {
            if (command == produceCommand)
            {
                CurrentResources += produceByStat.currentValue;
            }
            if (command == transferFromCommand)
            {
                ReceiveResources(transferFrom, transferFrom.ReleaseResources(capacityStat.currentValue));
            }
            if (command == transferToCommand)
            {
                var resources = ReleaseResources();
                transferTo.ReceiveResources(this, resources);
            }
        }

        private void ReceiveResources(ResourceContainer from, float resources)
        {
            CurrentResources += resources;
            OnResourcesReceived?.Invoke(resources);
        }

        public float ReleaseResources(float amount = Mathf.Infinity)
        {
            var tempResources = Mathf.Min(CurrentResources, amount);
            CurrentResources -= tempResources;
            return tempResources;
        }
    }
}