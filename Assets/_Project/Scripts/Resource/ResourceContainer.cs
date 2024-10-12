using System;
using IdleGame.Command;
using TMPro;
using UnityEngine;

namespace IdleGame.Resource
{
    public class ResourceContainer : MonoBehaviour
    {
        [SerializeField] private float produceBy = 10f;
        [SerializeField] private ResourceContainer transferFrom;
        [SerializeField] private ResourceContainer transferTo;
        [SerializeField] private CommandRunner commandRunner;
        [SerializeField] private CommandBehavior produceCommand;
        [SerializeField] private CommandBehavior transferFromCommand;
        [SerializeField] private CommandBehavior transferToCommand;
        [SerializeField] private float _currentResources = 0f;
        [SerializeField] private TMP_Text resourceAmountText;
        [SerializeField] private float capacity= 50;

        private float CurrentResources
        {
            set
            {
                _currentResources = Mathf.Min(capacity, value);
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
            if(command == produceCommand) CurrentResources += produceBy;
            if (command == transferFromCommand)
            {
                ReceiveResources(transferFrom, transferFrom.ReleaseResources());
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
        }

        public float ReleaseResources()
        {
            var tempResources = CurrentResources;
            CurrentResources = 0f;
            return tempResources;
        }
    }
}