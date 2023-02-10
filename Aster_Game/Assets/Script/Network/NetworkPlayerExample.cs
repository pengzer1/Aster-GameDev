using UnityEngine;
using Unity.Netcode;

namespace AG.Network
{
    // Custom struct can be in NetworkVariable
    public struct CustomStructVariable : INetworkSerializable
    {
        public int intValue;
        public float floatValue;
        public Unity.Collections.FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref intValue);
            serializer.SerializeValue(ref floatValue);
        }
    }
    public class NetworkPlayerExample : NetworkBehaviour
    {
        // only value type can be T
        private NetworkVariable<int> randomNumber = new NetworkVariable<int>(
            0, 
            NetworkVariableReadPermission.Everyone, 
            NetworkVariableWritePermission.Owner
        );

        private NetworkVariable<CustomStructVariable> customValue = new NetworkVariable<CustomStructVariable>(
            new CustomStructVariable{intValue = 0, floatValue = 0.0f}, 
            NetworkVariableReadPermission.Everyone, 
            NetworkVariableWritePermission.Owner
        );

        public override void OnNetworkSpawn()
        {
            randomNumber.OnValueChanged += (int previousValue, int changedValue) => {
                Debug.Log($"{OwnerClientId}'s random value changed from {previousValue} to {changedValue}");
            };
        }

        private void Update()
        {
            if(!IsOwner)  return;

            Vector3 moveDir = new Vector3();

            if(Input.GetKey(KeyCode.W))
            {
                moveDir.x += 1.0f;
            }
            if(Input.GetKey(KeyCode.S))
            {
                moveDir.x -= 1.0f;
            }
            if(Input.GetKey(KeyCode.A))
            {
                moveDir.z -= 1.0f;
            }
            if(Input.GetKey(KeyCode.D))
            {
                moveDir.z += 1.0f;
            }

            transform.position += moveDir * 3.0f * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.T))
            {
                SpawnObject();
            }
        }

        [SerializeField]
        private Transform spawningObject;
        private Transform spawnedObject;

        private void SpawnObject()
        {
            spawnedObject = Instantiate(spawningObject);
            spawnedObject.GetComponent<NetworkObject>().Spawn(true);
        }

        [ServerRpc]
        public void TestServerRPC()
        {
            Debug.Log($"this function will run in host(server)");
        }

        [ClientRpc]
        public void TestClientRPC()
        {
            Debug.Log($"Can only server invoke this function! And to all client, include host");
        }
    }
}