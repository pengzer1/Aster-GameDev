using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;

namespace AG.Network.AGLobby
{
    public class LobbyTest : MonoBehaviour
    {
        private Lobby hostLobby;

        private Lobby joinedLobby;

        private float lobbyHeartbeatTimer = 0.0f;

        private float lobbyHeartbeatTimeMax = 25.0f;

        private float lobbyUpdateTimer = 0.0f;

        private string playerName;

        private async void Start()
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () => {Debug.Log($"Signed in : {AuthenticationService.Instance.PlayerId}");};
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            playerName = "Default Player Name" + UnityEngine.Random.Range(1, 100);
            Debug.Log($"{playerName}");
        }

        private void Update() 
        {
            MaintainLobbyAlive();

            HandleLobbyPollForUpdates();
        }

        [ContextMenu("CreateLobby")]
        private async void CreateLobby()
        {
            try 
            {
                string lobbyName = "Sample Lobby";
                int maxPlayers = 4;
                string gameMode = "SlashAnimals";
                string mapName = "DefaultMap";

                CreateLobbyOptions lobbyOptions = new CreateLobbyOptions{
                    IsPrivate = false,
                    Player = GetPlayer(),
                    Data = new Dictionary<string, DataObject>{
                        {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, gameMode) },
                        {"Map", new DataObject(DataObject.VisibilityOptions.Public, mapName) }
                    }
                };

                hostLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, lobbyOptions);
                joinedLobby = hostLobby;

                Debug.Log($"lobby created [{hostLobby.Name}], and lobby code is {hostLobby.LobbyCode}");
                PrintPlayers(hostLobby);
            }
            catch (LobbyServiceException e) 
            {
                Debug.Log($"{e}");
            }
        }

        private async void MaintainLobbyAlive()
        {
            if(hostLobby == null)   return;

            lobbyHeartbeatTimer += Time.deltaTime;
            if(lobbyHeartbeatTimer > lobbyHeartbeatTimeMax)
            {
                lobbyHeartbeatTimer = 0.0f;
                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }

        private async void HandleLobbyPollForUpdates()
        {
            if(joinedLobby == null) return;
            
            lobbyUpdateTimer += Time.deltaTime;
            if(lobbyUpdateTimer > 1.1f)
            {
                lobbyUpdateTimer = 0.0f;
                var lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby;
            }
        }


        [ContextMenu("Search Lobbies")]
        private async void SearchLobbies()
        {            
            try 
            {
                string gameMode = "SlashAnimals";
                QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions{
                    Count = 25,
                    Filters = new List<QueryFilter>{ 
                        new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT),
                        new QueryFilter(QueryFilter.FieldOptions.S1, gameMode, QueryFilter.OpOptions.EQ) 
                    },
                    Order = new List<QueryOrder>{ new QueryOrder(false, QueryOrder.FieldOptions.Created) }
                };

                QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

                Debug.Log($"Found {queryResponse.Results.Count} Lobbies");
                foreach(var lobby in queryResponse.Results)
                {
                    Debug.Log($"[Lobby] {lobby.Name} , max player : {lobby.MaxPlayers}, mode : {lobby.Data["GameMode"].Value}");
                }
            }
            catch (LobbyServiceException e) 
            {
                Debug.Log($"{e}");
            }
        }

        [ContextMenu("Find Lobby")]
        private async void JoinLobby()
        {
            try 
            {
                QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
                
                await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);

                Debug.Log($"Join Lobby with Id");
            }
            catch (LobbyServiceException e) 
            {
                Debug.Log($"{e}");
            }
        }

        private async void JoinLobbyByCode(string lobbyCode)
        {
            try 
            {
                JoinLobbyByCodeOptions joinLobbyOptions = new JoinLobbyByCodeOptions{
                    Player = GetPlayer()
                };
                joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyOptions);
                
                Debug.Log($"Join Lobby with code {lobbyCode}");

                PrintPlayers(joinedLobby);
            }
            catch (LobbyServiceException e) 
            {
                Debug.Log($"{e}");
            }
        }

        [ContextMenu("Quick Match")]
        private async void QuickMatch()
        {
            try
            {
                await LobbyService.Instance.QuickJoinLobbyAsync();
            }
            catch (LobbyServiceException e) 
            {
                Debug.Log($"{e}");
            }
        }

        private void PrintPlayers()
        {
            PrintPlayers(joinedLobby);
        }

        private void PrintPlayers(Lobby lobby)
        {
            Debug.Log($"======= Players in {lobby.Name} lobby, mode [{lobby.Data["GameMode"].Value}], map [{lobby.Data["Map"].Value}]");
            foreach(var player in lobby.Players)
            {
                Debug.Log($"player id : {player.Id} and name {player.Data["playerName"].Value}");
            }
        }

        private Player GetPlayer()
        {
            return new Player{
                Data = new Dictionary<string, PlayerDataObject>{
                    {"playerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName)}
                }
            };
        }

        private async void HostUpdateGameMode(string gameMode)
        {
            try
            {
                hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions{
                    Data = new Dictionary<string, DataObject>{
                        {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, gameMode)}
                    }
                });
                joinedLobby = hostLobby;

                PrintPlayers(hostLobby);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        private async void UpdatePlayerName(string newPlayerName)
        {
            try
            {
                playerName = newPlayerName;
                await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions{
                    Data = new Dictionary<string, PlayerDataObject>{
                        {"playerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName)}
                    }
                });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        private async void LeaveLobby()
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        private async void KickPlayer()
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, joinedLobby.Players[1].Id);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        private async void MigrateHost()
        {
            try
            {
                hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions{
                    HostId = joinedLobby.Players[1].Id
                });
                joinedLobby = hostLobby;

                PrintPlayers(hostLobby);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        private async void DeleteLobby()
        {
            try
            {
                await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }
    }
}