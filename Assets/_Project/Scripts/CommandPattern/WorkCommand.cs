using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IdleGame.Command
{
    public class WorkCommand : CommandBehavior
    {
        [SerializeField] private GameObject progressBarParent;
        [SerializeField] private Image progressBar;
        [SerializeField] private Stat workDurationStat;
        private Transform _commander;

        public override void Init(Transform commander)
        {
            _commander = commander;
        }

        public override IEnumerator Execute()
        {
            progressBarParent.SetActive(true);
            var elapsed = 0f;

            while (elapsed < workDurationStat.currentValue)
            {
                progressBar.fillAmount = elapsed / workDurationStat.currentValue;
                elapsed += Time.deltaTime;
                yield return null; 
            }

            progressBar.fillAmount = 1f;
            progressBarParent.SetActive(false);
        }
    }
}