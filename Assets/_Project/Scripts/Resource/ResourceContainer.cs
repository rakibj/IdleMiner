using System;
using IdleGame.Command;
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
            if(command == produceCommand) _currentResources += produceBy;
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
            Debug.Log(gameObject.name + " received " + resources + " from " + from.gameObject.name);
            _currentResources += resources;
        }

        public float ReleaseResources()
        {
            var tempResources = _currentResources;
            _currentResources = 0f;
            return tempResources;
        }
    }
}