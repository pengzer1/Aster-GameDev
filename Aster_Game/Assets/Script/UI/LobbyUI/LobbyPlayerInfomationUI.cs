using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies.Models;
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
            this.player = player;
            // TODO : Quick match 사용시 오류 나옴
            playerNameText.text = player.Data[NetworkConstants.PLAYERNAME_KEY].Value;
        }

        private void KickPlayer()
        {
            // TODO : 방장 스스로 차면 생기는 버그 해결
            if(player == null)  return;

            LobbySingleton.instance.KickPlayer(player.Id);
        }
    }
}