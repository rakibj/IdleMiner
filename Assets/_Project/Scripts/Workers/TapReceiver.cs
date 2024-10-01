using System;
using UnityEngine;

namespace IdleGame.Workers
{
    public class TapReceiver : MonoBehaviour
    {
        public Action OnTapped;

        public void Tap()
        {
            OnTapped?.Invoke();
        }
    }
}