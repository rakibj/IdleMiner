using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame.Command
{
    public abstract class CommandBehavior : MonoBehaviour
    {
        public abstract void Init(Transform commander);
        public abstract IEnumerator Execute();
    }
}