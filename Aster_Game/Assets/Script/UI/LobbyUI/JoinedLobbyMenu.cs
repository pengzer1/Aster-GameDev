using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies.Models;
using AG.GameLogic.ObjectPooling;
using AG.Network.AGLobby;

namespace AG.UI.LobbyUI
{
    public class JoinedLobbyMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject lobbySetupMenu; 
        [SerializeField]
        private GameObject lobbyCreateMenu; 
        [SerializeField]
        private IObjectPool playerInfomationPool;
        [SerializeField]
        private TextMeshProUGUI lobbyNameText;
        [SerializeField]
        private TextMeshProUGUI playerCountText;
        [SerializeField]
        private TextMeshProUGUI maxPlayerCountText;
        [SerializeField]
        private Transform playerListContainer;
        [SerializeField]
        private Button leaveLobbyButton;
        [SerializeField]
        private Button startGameButton;

        private void Awake()
        {
            playerInfomationPool = GetComponent<IObjectPool>();
            
            leaveLobbyButton.onClick.AddListener(()=>{
                LobbySingleton.instance.LeaveLobby();
                lobbySetupMenu.SetActive(true);
                this.gameObject.SetActive(false);
            });  

            startGameButton.onClick.AddListener(() => {
                LobbySingleton.instance.StartGame();
            });
        }

        private void Start()
        {
            LobbySingleton.instance.joinLobbyEvent += UpdateLobbyInfomation;
            LobbySingleton.instance.gameStartEvent += () => { 
                this.gameObject.SetActive(false);
                this.transform.parent.gameObject.SetActive(false); 
            };

            LobbySingleton.instance.kickedFromLobbyEvent += () => {
                this.gameObject.SetActive(false);
            };

            LobbySingleton.instance.leaveLobbyEvent += () => {
                this.gameObject.SetActive(false);
            };

            this.gameObject.SetActive(false);
        }

        public void UpdateLobbyInfomation(Lobby lobby)
        {
            SetupMenus();
            SetPlayerInfomations(lobby);
            SetTextInfomation(lobby);
            DeleteStartGameButtonIfNotHost();
        }

        private void SetupMenus()
        {
            lobbySetupMenu.SetActive(false);
            lobbyCreateMenu.SetActive(false);
            this.gameObject.SetActive(true);
        }

        private void SetPlayerInfomations(Lobby lobby)
        {
            playerInfomationPool.ReturnAllObjects();

            foreach (var player in lobby.Players)
            {
                var playerInfomation = playerInfomationPool.GetObjectFromPool();
                playerInfomation.transform.SetParent(playerListContainer);
                playerInfomation.GetComponent<LobbyPlayerInfomationUI>().SetPlayerInfo(player);
            }
        }

        private void SetTextInfomation(Lobby lobby)
        {
            lobbyNameText.text = lobby.Name;
            playerCountText.text = lobby.Players.Count.ToString();
            maxPlayerCountText.text = lobby.MaxPlayers.ToString();
        }

        private void DeleteStartGameButtonIfNotHost()
        {
            if(LobbySingleton.instance.IsLobbyhost())
            {
                startGameButton.gameObject.SetActive(true);
                return;
            }
            startGameButton.gameObject.SetActive(false);
        }
    }
}