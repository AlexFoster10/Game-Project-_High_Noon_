using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseMenu.gameIsPaused = false;
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void menu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
