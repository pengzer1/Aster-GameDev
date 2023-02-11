using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace AG.UI.MultiplayUI
{
    public class MultiplayInGameMenu : MonoBehaviour
    {        
        [SerializeField]
        private Button inGameMenuButton;

        [Header("Menu")]
        [SerializeField]
        private GameObject multiplayMenu;
        [SerializeField]
        private Button leaveGameButton;
        [SerializeField]
        private Button resumeGameButton;
        
        private bool nowMenuIsActive;

        private void Start()
        {
            ButtonSetup();
        }

        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.Escape))    return;

            SetInGameMenu(!nowMenuIsActive);
        }

        private void ButtonSetup()
        {
            inGameMenuButton.onClick.AddListener(() =>
            {
                multiplayMenu.SetActive(true);
            });

            leaveGameButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.Shutdown();
                // TODO: return to main menu
            });

            resumeGameButton.onClick.AddListener(() =>
            {
                multiplayMenu.SetActive(false);
            });
        }

        private void SetInGameMenu(bool isActive)
        {
            nowMenuIsActive = isActive;
        }
    }
}