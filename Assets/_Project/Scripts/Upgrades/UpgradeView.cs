using System;
using IdleGame.Workers;
using UnityEngine;
using UnityEngine.UI;

namespace IdleGame.Upgrades
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TapReceiver tapReceiver;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            tapReceiver.OnTapped += Show;
            closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            tapReceiver.OnTapped -= Show;
            closeButton.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            canvas.enabled = true;
        }

        public void Hide()
        {
            canvas.enabled = false;
        }
    }
}