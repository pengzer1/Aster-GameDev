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

        public void UpdateLobbyInfomation(Lobby lobby)
        {
            SetPlayerInfomations(lobby);
            SetTextInfomation(lobby);
        }

        private void SetPlayerInfomations(Lobby lobby)
        {
            playerInfomationPool.ReturnAllObjects();

            foreach (var player in lobby.Players)
            {
                var playerInfomation = playerInfomationPool.GetObjectFromPool();
                playerInfomation.transform.SetParent(playerListContainer);
                // playerInfomation.GetComponent<>().
            }
        }

        private void SetTextInfomation(Lobby lobby)
        {
            lobbyNameText.text = lobby.Name;
            playerCountText.text = lobby.Players.Count.ToString();
            maxPlayerCountText.text = lobby.MaxPlayers.ToString();
        }
    }
}