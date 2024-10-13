using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleGame.Upgrades
{
    public class UpgradeRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentText;
        [SerializeField] private TMP_Text upgradeByText;

        public void UpdateView(string current, string upgradeBy)
        {
            currentText.text = current;
            upgradeByText.text = upgradeBy;
        }
    }
}