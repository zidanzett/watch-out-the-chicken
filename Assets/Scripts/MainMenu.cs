using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public Button playButton;
    public Button exitButton;
    public Toggle audioToggle;

    private void Start() {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        audioToggle.onValueChanged.AddListener(OnAuidoToggleValue);
    }

    private void OnPlayButtonClicked() {
        SceneManager.LoadScene("GameScene");
    }

    private void OnExitButtonClicked() {
        Application.Quit();
    }

    private void OnAuidoToggleValue(bool isOn) {
        if (isOn) {
            audioSource.volume = 0.5f;
        }
        else {
            audioSource.volume = 0.0f;
        }
    }
}
