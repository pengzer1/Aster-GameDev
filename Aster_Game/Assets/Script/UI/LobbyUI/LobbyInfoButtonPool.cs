using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AG.GameLogic.ObjectPooling;

namespace AG.UI.LobbyUI
{
    public class LobbyInfoButtonPool : MonoBehaviour, IObjectPool
    {
        private Queue<PoolableObject> buttonPool = new Queue<PoolableObject>();

        private Queue<PoolableObject> outbuttons = new Queue<PoolableObject>();
        
        [SerializeField]
        private PoolableObject buttonPrefab;

        public PoolableObject GetObjectFromPool()
        {
            if(buttonPool.Count <= 0)
            {
                SupplyObjectPool();
            }

            var button = buttonPool.Dequeue();

            button.gameObject.SetActive(true);
            button.transform.localScale = new Vector3(1, 1, 1);
            outbuttons.Enqueue(button);
            return button;
        }

        public void InsertObjectToPool(PoolableObject obj)
        {
            obj.gameObject.transform.SetParent(this.gameObject.transform);
            obj.gameObject.SetActive(false);
            
            buttonPool.Enqueue(obj);
        }

        public void SupplyObjectPool()
        {
            var button = Instantiate(buttonPrefab);
            button.ReturnToPoolCallbackEvent += InsertObjectToPool;

            InsertObjectToPool(button);
        }

        public void ResetAllLobbyButtons()
        {
            while(outbuttons.Count > 0)
            {
                outbuttons.Dequeue().InvokeReturnCall();
            }
        }
    }
}