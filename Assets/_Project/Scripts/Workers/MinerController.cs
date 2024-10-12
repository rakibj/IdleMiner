using System;
using IdleGame.Command;
using UnityEngine;

namespace IdleGame.Workers
{
    public class MinerController : MonoBehaviour
    {
        [SerializeField] private CommandRunner commandRunner;
        [SerializeField] private TapReceiver tapReceiver;
        [SerializeField] private float resourceGainPerMine = 10f;
        [SerializeField] private CommandBehavior dropCommand;
        
        private void OnEnable()
        {
            tapReceiver.OnTapped += Run;
            commandRunner.OnCommandComplete += OnCommandComplete;
        }

        private void OnDisable()
        {
            tapReceiver.OnTapped -= Run;
            commandRunner.OnCommandComplete -= OnCommandComplete;
        }
        private void OnCommandComplete(CommandBehavior command)
        {
            if (command == dropCommand)
            {
                //TODO pass this resource to a receiver
                Debug.Log("Drop resource to receiver " + resourceGainPerMine);
            }
        }

        private void Run()
        {
            commandRunner.ResetAndRun();
        }
    }
}