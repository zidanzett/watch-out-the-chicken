using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Button restartBtn;
    public Button mainMenuBtn;

    private void Start() {
        restartBtn.onClick.AddListener(OnRestartButtonClicked);
        mainMenuBtn.onClick.AddListener(OnMainMenuButtonClicked);

    }

    private void OnRestartButtonClicked() {
        SceneManager.LoadScene("GameScene");
    }

    private void OnMainMenuButtonClicked() {
        SceneManager.LoadScene("MainMenu");
    }
}
