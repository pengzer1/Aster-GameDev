using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using AG.Network;
using AG.Network.AGLobby;
using AG.GameLogic.ObjectPooling;

namespace AG.UI.LobbyUI
{
    public class LobbyPlayerInfomationUI : PoolableObject
    {
        [SerializeField]
        private TextMeshProUGUI playerNameText;
        [SerializeField]
        private Button kickplayerButton;
        private Player player;

        private void Awake()
        {
            kickplayerButton.onClick.AddListener(() => {
                KickPlayer();
            });
        }

        public void SetPlayerInfo(Player player)
        {
            Debug.Log($"SetPlayerInfo");
            this.player = player;
            playerNameText.text = player.Data[NetworkConstants.PLAYERNAME_KEY].Value;

            bool noAuthorityOrHostItself = !LobbySingleton.instance.IsLobbyhost() || player.Id == AuthenticationService.Instance.PlayerId;
            kickplayerButton.gameObject.SetActive(!noAuthorityOrHostItself);
        }

        private void KickPlayer()
        {
            if(player == null)  return;

            LobbySingleton.instance.KickPlayer(player.Id);
        }
    }
}