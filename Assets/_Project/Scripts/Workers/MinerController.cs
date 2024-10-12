using System;
using IdleGame.Command;
using UnityEngine;

namespace IdleGame.Workers
{
    public class MinerController : MonoBehaviour
    {
        [SerializeField] private CommandRunner commandRunner;
        [SerializeField] private TapReceiver tapReceiver;

        private void OnEnable()
        {
            tapReceiver.OnTapped += Run;
        }

        private void OnDisable()
        {
            tapReceiver.OnTapped -= Run;
        }

        private void Run()
        {
            commandRunner.ResetAndRun();
        }
    }
}