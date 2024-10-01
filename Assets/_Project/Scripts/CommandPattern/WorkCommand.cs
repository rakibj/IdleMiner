using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IdleGame.Command
{
    public class WorkCommand : CommandBehavior
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private float workDuration;
        private Transform _commander;

        public override void Init(Transform commander)
        {
            _commander = commander;
        }

        public override IEnumerator Execute()
        {
            var elapsed = 0f;

            while (elapsed < workDuration)
            {
                progressBar.fillAmount = elapsed / workDuration;
                elapsed += Time.deltaTime;
                yield return null; 
            }

            progressBar.fillAmount = 1f;
        }
    }
}