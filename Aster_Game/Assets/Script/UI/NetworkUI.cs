using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace AG.UI
{
    public class NetworkUI : MonoBehaviour
    {
        [SerializeField]
        private Button serverButton;
        [SerializeField]
        private Button hostButton;
        [SerializeField]
        private Button clientButton;

        private void Awake()
        {
            serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); this.gameObject.SetActive(false); });
            hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); this.gameObject.SetActive(false); });
            clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); this.gameObject.SetActive(false); });
        }
    }
}

