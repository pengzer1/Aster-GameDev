using UnityEngine;
using System;

namespace AG.GameLogic.ObjectPooling
{
    public class PoolableObject : MonoBehaviour
    {
        public event Action<PoolableObject> ReturnToPoolCallbackEvent;

        public void InvokeReturnCall()
        {
            ReturnToPoolCallbackEvent?.Invoke(this);
        }
    }
}