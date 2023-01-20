using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using AG.Network.AGLobby;

namespace AG.UI.LobbyUI
{
    public class LobbyListMenu : MonoBehaviour
    {
        [SerializeField]
        private LobbyInfoButtonUI lobbyButtonElement;

        [SerializeField]
        private Transform listContainer;

        [SerializeField]
        private Button cancleButton;

        [SerializeField]
        private Transform lobbySetupMenu;

        private void Start()
        {
            LobbySingleton.instance.lobbyListChangedEvent += RefreshLobbyListUI;

            cancleButton.onClick.AddListener(() => {
                lobbySetupMenu.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            });
        }

        private void RefreshLobbyListUI(List<Lobby> lobbyList)
        {
            // TODO : to object pool
            foreach(Transform child in listContainer)
            {
                Destroy(child.gameObject);
            }

            foreach(var lobby in lobbyList)
            {
                Instantiate(lobbyButtonElement, listContainer).UpdateLobbyButtonInfo(lobby);
            }
        } 
    }
}