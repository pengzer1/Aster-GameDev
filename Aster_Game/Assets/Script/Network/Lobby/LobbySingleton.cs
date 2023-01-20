using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using AG.Network;


namespace AG.Network.AGLobby
{
    public class LobbySingleton : MonoBehaviour
    {
        public static LobbySingleton instance { get; private set; }

        public event Action<List<Lobby>> lobbyListChangedEvent;

        public event Action<Lobby> joinLobbyEvent;

        private Lobby hostLobby;

        private Lobby joinedLobby;

        private string playerName;

        private float lobbyMaintainTimer = 0.0f;

        private float lobbyInfomationUpdateTimer = 0.0f;

        private void Awake()
        {
            instance = this;
        }

        private async void Start()
        {
            // TODO : 인증 시스템 따로 제작
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () => {Debug.Log($"Signed in : {AuthenticationService.Instance.PlayerId}");};
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            playerName = "Default Player Name" + UnityEngine.Random.Range(1, 100);
            Debug.Log($"{playerName}");
        }

        private void Update()
        {
            MaintainLobbyAlive();
            RefreshLobbyInfomation();
        }

        public async void CreateLobby(string lobbyName, int maxPlayers, bool isPrivate)
        {
            CreateLobbyOptions createOptions = new CreateLobbyOptions{
                Player = GetPlayer(),
                IsPrivate = isPrivate,
                Data = new Dictionary<string, DataObject>{
                    {NetworkConstants.GAMEMODE_KEY, new DataObject(DataObject.VisibilityOptions.Public, "DefaultGameMode")}
                }
            };

            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createOptions);
            joinedLobby = lobby;

            joinLobbyEvent?.Invoke(lobby);

            Debug.Log($"Create lobby {joinedLobby.Name}");
        }
        
        public async void MaintainLobbyAlive()
        {
            if(!IsLobbyhost())  return;

            lobbyMaintainTimer += Time.deltaTime;
            if(lobbyMaintainTimer < NetworkConstants.LOBBY_MAINTAIN_TIME) return;

            lobbyMaintainTimer = 0.0f;
            await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
        }

        public async void RefreshLobbyInfomation()
        {
            if(joinedLobby == null) return;

            lobbyInfomationUpdateTimer += Time.deltaTime;
            if(lobbyInfomationUpdateTimer < NetworkConstants.LOBBY_INFO_UPDATE_TIME)   return;

            lobbyInfomationUpdateTimer = 0.0f;
            var lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
            joinedLobby = lobby;
            // TODO : refresh ui
            // TODO : handle kicked
        }

        public async void JoinLobbyByUI(Lobby lobby)
        {
            var joinOption = new JoinLobbyByIdOptions{ Player = GetPlayer() };
            joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id, joinOption);

            joinLobbyEvent?.Invoke(joinedLobby);
        }

        public async void JoinLobbyByCode(string lobbyCode)
        {
            var joinOption = new JoinLobbyByCodeOptions{ Player = GetPlayer() };
            joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, joinOption);

            joinLobbyEvent?.Invoke(joinedLobby);
        }

        public async void QuickMatch()
        {
            try {
                QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

                Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
                joinedLobby = lobby;

                joinLobbyEvent?.Invoke(joinedLobby);
            } 
            catch (LobbyServiceException e) 
            {
                Debug.Log(e);
            }
        }

        public async void GetLobbyList()
        {
            try
            {
                QueryLobbiesOptions options = new QueryLobbiesOptions{
                    Count = 25,
                    Filters = new List<QueryFilter>{
                        new QueryFilter(
                            field: QueryFilter.FieldOptions.AvailableSlots,
                            op: QueryFilter.OpOptions.GT,
                            value: "0"
                        )
                    },
                    Order = new List<QueryOrder>{
                        new QueryOrder(
                            asc: false,
                            field: QueryOrder.FieldOptions.Created
                        )
                    }
                };

                QueryResponse lobbyListQueryResponse = await Lobbies.Instance.QueryLobbiesAsync();
                lobbyListChangedEvent?.Invoke(lobbyListQueryResponse.Results);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        public async void LeaveLobby()
        {
            if(joinedLobby == null) return;

            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
                joinedLobby = null;
                // TODO : leave lobby event;
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        public async void KickPlayer(string playerId)
        {
            if(!IsLobbyhost())  return;

            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
            }
            catch (LobbyServiceException e) 
            {
                Debug.Log($"{e}");
            }
        } 

        private Player GetPlayer()
        {
            return new Player{
                Data = new Dictionary<string, PlayerDataObject>{
                    {NetworkConstants.PLAYERNAME_KEY, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName)}
                }
            };
        }

        private async void MigrateHost()
        {
            try
            {
                hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions{
                    HostId = joinedLobby.Players[1].Id
                });
                joinedLobby = hostLobby;
                
                // TODO : refresh lobby
            }
            catch (LobbyServiceException e)
            {
                Debug.Log($"{e}");
            }
        }

        private bool IsLobbyhost()
        {
            return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
        }
    }
}
