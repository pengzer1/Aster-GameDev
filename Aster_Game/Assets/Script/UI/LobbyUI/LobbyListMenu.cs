using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using AG.Network.AGLobby;

namespace AG.UI.LobbyUI
{
    public class LobbyListMenu : MonoBehaviour
    {
        // [SerializeField]
        // private LobbyInfoButtonUI lobbyButtonElement;

        [SerializeField]
        private Transform listContainer;

        [SerializeField]
        private LobbyInfoButtonPool lobbyInfoButtonPool;

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
            // foreach(Transform child in listContainer)
            // {
            //     Destroy(child.gameObject);
            // }
            lobbyInfoButtonPool.ResetAllLobbyButtons();

            foreach(var lobby in lobbyList)
            {
                var button = lobbyInfoButtonPool.GetObjectFromPool();
                button.transform.SetParent(listContainer);
                button.GetComponent<LobbyInfoButtonUI>().UpdateLobbyButtonInfo(lobby);
                // Instantiate(lobbyButtonElement, listContainer).UpdateLobbyButtonInfo(lobby);
            }
        } 
    }
}