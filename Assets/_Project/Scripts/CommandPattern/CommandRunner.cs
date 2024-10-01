using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame.Command
{
    public class CommandRunner : MonoBehaviour
    {
        [SerializeField] private bool autoRun = false;
        [SerializeField] private List<CommandBehavior> startCommands = new();  
        private List<CommandBehavior> _commandList = new();  
        private bool _isRunning = false;
        private Coroutine _currentCommandCoroutine;

        private void Start()
        {
            foreach (var command in startCommands)
                AddCommand(command);
            
            if (autoRun)
                Run();
        }

        public void AddCommand(CommandBehavior command)
        {
            _commandList.Add(command);
            command.Init(transform);
        }

        public void Run()
        {
            if (!_isRunning)
            {
                StartCoroutine(RunCommands());
            }
        }

        private IEnumerator RunCommands()
        {
            _isRunning = true;

            while (_commandList.Count > 0)
            {
                var command = _commandList[0]; 
                _currentCommandCoroutine = StartCoroutine(command.Execute());
                yield return _currentCommandCoroutine; 

                _commandList.RemoveAt(0);  
            }

            _isRunning = false;
        }

        public void StopCommand()
        {
            if (_currentCommandCoroutine != null)
            {
                StopCoroutine(_currentCommandCoroutine);
                _currentCommandCoroutine = null;
            }

            _isRunning = false;
        }
    }

}