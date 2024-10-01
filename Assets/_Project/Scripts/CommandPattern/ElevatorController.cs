using IdleGame.Workers;
using UnityEngine;

namespace IdleGame.Command
{
    public class ElevatorController : MonoBehaviour
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