using System.Collections.Generic;
using UnityEngine;
using AG.GameLogic.ObjectPooling;

namespace AG.Network.AGLobby
{
    public class PlayerInfomationButtonPool : MonoBehaviour, IObjectPool
    {
        private Queue<PoolableObject> buttonPool = new Queue<PoolableObject>();
        // TODO : 이 아웃버튼이 큰 문제 없는지 확인
        private Queue<PoolableObject> outbuttons = new Queue<PoolableObject>();

        [SerializeField]
        private PoolableObject playerInfomationButtonPrefab;

        public PoolableObject GetObjectFromPool()
        {
            if(buttonPool.Count <= 0)
            {
                SupplyObjectPool();
            }

            var playerInfomationButton = buttonPool.Dequeue();
            playerInfomationButton.gameObject.SetActive(true);
            outbuttons.Enqueue(playerInfomationButton);
            playerInfomationButton.transform.localScale = new Vector3(1, 1, 1);

            return playerInfomationButton;
        }

        public void InsertObjectToPool(PoolableObject obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(this.transform);

            buttonPool.Enqueue(obj);
        }

        public void SupplyObjectPool()
        {
            var playerInfomationButton = Instantiate(playerInfomationButtonPrefab);
            playerInfomationButton.ReturnToPoolCallbackEvent += InsertObjectToPool;
            InsertObjectToPool(playerInfomationButton);
        }

        public void ReturnAllObjects()
        {
            while(outbuttons.Count > 0)
            {
                outbuttons.Dequeue().InvokeReturnCall();
            }
        }
    }
}