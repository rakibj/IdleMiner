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
        public Action<CommandBehavior> OnCommandCompleted;

        private void Start()
        {
            if (autoRun) ResetAndRun();
        }

        public void AddCommand(CommandBehavior command)
        {
            _commandList.Add(command);
            command.Init(transform);
        }

        public void ResetAndRun()
        {
            if (_isRunning) return;

            foreach (var command in startCommands)
                AddCommand(command);
            Run();
        }
        public void Run()
        {
            if (_isRunning) return;
            StartCoroutine(RunCommands());
        }

        private IEnumerator RunCommands()
        {
            _isRunning = true;

            while (_commandList.Count > 0)
            {
                var command = _commandList[0]; 
                _currentCommandCoroutine = StartCoroutine(command.Execute());
                yield return _currentCommandCoroutine; 

                OnCommandCompleted?.Invoke(command);
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