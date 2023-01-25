using UnityEngine;
using UnityEngine.UI;
using AG.Network.AGLobby;

namespace AG.UI.LobbyUI
{
    public class LobbySetupMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField]
        private Button createLobbyButton;

        [SerializeField]
        private Button searchLobbyButton;

        [SerializeField]
        private Button quickMatchButton;

        [Header("Under Menus")]
        [SerializeField]
        private GameObject createLobbyMenu;
        
        [SerializeField]
        private GameObject lobyListMenu;

        private void Start()
        {
            createLobbyButton.onClick.AddListener(() => {
                createLobbyMenu.SetActive(true);
                this.gameObject.SetActive(false);
            });

            searchLobbyButton.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
                lobyListMenu.SetActive(true);
                LobbySingleton.instance.GetLobbyList();
            });

            quickMatchButton.onClick.AddListener(() => {
                LobbySingleton.instance.QuickMatch();
            });

            createLobbyMenu.SetActive(false);
            this.gameObject.SetActive(false);
        }


    }
}