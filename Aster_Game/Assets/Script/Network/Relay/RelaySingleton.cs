using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using AG.Network.AGLobby;

namespace AG.Network.AGRelay
{
    public class RelaySingleton : MonoBehaviour
    {
        public static async Task<string> CreateRelay()
        {
            try
            {
                var playerCount = LobbySingleton.instance.GetJoinedLobby().MaxPlayers - 1;
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(playerCount);

                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

                Debug.Log($"join code is : {joinCode}");

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                    allocation.RelayServer.IpV4,
                    (ushort)allocation.RelayServer.Port,
                    allocation.AllocationIdBytes,
                    allocation.Key,
                    allocation.ConnectionData
                );

                NetworkManager.Singleton.StartHost();

                return joinCode;
            }
            catch(RelayServiceException e)
            {
                Debug.Log($"{e}");
                return null;
            }
        }

        public static async void JoinRelay(string joinRelayCode)
        {
            
            try
            {
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinRelayCode);

                Debug.Log($"joined relay by : {joinRelayCode}");

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                    joinAllocation.RelayServer.IpV4,
                    (ushort)joinAllocation.RelayServer.Port,
                    joinAllocation.AllocationIdBytes,
                    joinAllocation.Key,
                    joinAllocation.ConnectionData,
                    joinAllocation.HostConnectionData
                );

                NetworkManager.Singleton.StartClient();
            }
            catch(RelayServiceException e)
            {
                Debug.Log($"{e}");
            }
        }
    }
}