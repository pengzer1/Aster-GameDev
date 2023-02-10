using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using AG.Network.AGLobby;
using AG.GameLogic.ObjectPooling;

namespace AG.UI.LobbyUI
{
    public class LobbyListMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform listContainer;
        [SerializeField]
        private IObjectPool lobbyInfoButtonPool;
        [SerializeField]
        private Button cancleButton;
        [SerializeField]
        private GameObject lobbySetupMenu;

        private void Awake()
        {
            lobbyInfoButtonPool = GetComponent<IObjectPool>();
        }

        private void Start()
        {
            LobbySingleton.instance.lobbyListChangedEvent += RefreshLobbyListUI;
            LobbySingleton.instance.joinLobbyEvent += PlayerJoinLobby;

            cancleButton.onClick.AddListener(() => {
                lobbySetupMenu.SetActive(true);
                this.gameObject.SetActive(false);
            });

            this.gameObject.SetActive(false);
        }

        private void RefreshLobbyListUI(List<Lobby> lobbyList)
        {
            lobbyInfoButtonPool.ReturnAllObjects();

            foreach(var lobby in lobbyList)
            {
                var button = lobbyInfoButtonPool.GetObjectFromPool();
                button.transform.SetParent(listContainer);
                button.GetComponent<LobbyInfoButtonUI>().UpdateLobbyButtonInfo(lobby);
            }
        } 

        private void PlayerJoinLobby(Lobby lobby)
        {
            this.gameObject.SetActive(false);
        }
    }
}