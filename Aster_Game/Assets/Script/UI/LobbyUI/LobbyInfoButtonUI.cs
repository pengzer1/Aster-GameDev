using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;

namespace AG.UI.LobbyUI
{
    public class LobbyInfoButtonUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI lobbyNameText;

        [SerializeField]
        private TextMeshProUGUI lobbyPlayersText;

        [SerializeField]
        private TextMeshProUGUI lobbyMaxPlayersText;

        private Lobby lobby;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => {});
        }

        public void UpdateLobbyButtonInfo(Lobby lobbyButton)
        {
            lobby = lobbyButton;

            lobbyNameText.text = lobby.Name;
            lobbyPlayersText.text = lobby.Players.Count.ToString();
            lobbyMaxPlayersText.text = lobby.MaxPlayers.ToString();
        }
    }
}