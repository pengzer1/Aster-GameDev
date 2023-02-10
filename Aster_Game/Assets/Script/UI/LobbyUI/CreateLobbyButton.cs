using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AG.Network.AGLobby;

namespace AG.UI.LobbyUI
{
    public class CreateLobbyButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject lobbySetupMenu;
    
        [Header("Create Lobby Elements")]
        [SerializeField]
        private Button createGameButton;
        [SerializeField]
        private Button cancleGameButton;
        [SerializeField]
        private TMP_InputField lobbyNameText;
        [SerializeField]
        private Toggle isPrivate;
        [SerializeField]
        private Slider playerCountSlider;

        private void Start()
        {
            cancleGameButton.onClick.AddListener(() => {
                SetLobbySetupMenu(true);
                this.gameObject.SetActive(false);
            });

            createGameButton.onClick.AddListener(() => {
                LobbySingleton.instance.CreateLobby(lobbyNameText.text, (int)playerCountSlider.value, isPrivate.isOn);
            });
        }

        private void SetLobbySetupMenu(bool isActive)
        {
            lobbySetupMenu.SetActive(isActive);
        }
    }
}