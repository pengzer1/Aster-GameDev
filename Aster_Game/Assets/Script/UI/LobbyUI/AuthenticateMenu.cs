using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AG.Network.AGLobby;

namespace AG.UI.LobbyUI
{
    public class AuthenticateMenu : MonoBehaviour
    {
        [SerializeField]
        private Button submitButton;

        [SerializeField]
        private TMP_InputField nameInputField;

        [SerializeField]
        private GameObject lobbySetupMenu;

        private void Awake()
        {
            submitButton.onClick.AddListener(() => AuthenticateByInputFieldName());
        }

        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.Return))   return;

            AuthenticateByInputFieldName();   
        }

        private void AuthenticateByInputFieldName()
        {
            LobbySingleton.instance.Authenticate(nameInputField.text);
            lobbySetupMenu.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }
}