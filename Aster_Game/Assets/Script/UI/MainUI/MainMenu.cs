using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace AG.UI.MainUI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Properties")]
        private string soloplaySceneName;
        private string multiplaySceneName;

        [Header("UI Elements")]
        [SerializeField]
        private Button soloGameStartButton;
        [SerializeField]
        private Button multiplayStartButton;
        [SerializeField]
        private Button quitButton;

        private void Start()
        {
            ButtonSetup();
        }

        private void ButtonSetup()
        {
            soloGameStartButton.onClick.AddListener(() => SceneManager.LoadScene(soloplaySceneName));
            multiplayStartButton.onClick.AddListener(() => SceneManager.LoadScene(multiplaySceneName));
            quitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}