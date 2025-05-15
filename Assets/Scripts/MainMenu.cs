using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour

{
    public GameObject levelSelect;
    public GameObject homeScreen;
    public bool firstLaunch = true;
    string lvlName;
    int callCount = 1;
    GameObject child;
    private void Start()
    {
        if (gameObject.scene.name == "MainMenu")
        {
            callCount = 1;
            levelSelect = GameObject.Find("Level Select");
            homeScreen = GameObject.Find("Home Screen");
            onStartDeactivate();
            levelIconCheck();
            levelSelect.SetActive(false);
        }
    }
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

    public void levels()
    {
        homeScreen.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void onStartDeactivate()
    {
        for (int i = 0, count = levelSelect.transform.childCount - 1; i < count; i++)
        {
            //print("This Many Children");
            GameObject child = levelSelect.transform.GetChild(i).gameObject;
            {
                GameObject temp = child.transform.GetChild(0).gameObject;
                if (temp.name == "Pass")
                {
                    temp.SetActive(false);

                }
            }
        }
    }
    
    void loadLevel1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
    void loadLevel2()
    {
        if (levelCompletionCheck.level1Check)
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Level 2");
        }
    }
    void loadLevel3()
    {
        if (levelCompletionCheck.level2Check)
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Level 3");
        }
    }
    void levelIconCheck()
    {
        for (int i = 0, count = levelSelect.transform.childCount - 1; i < count; i++)
        {
            //print("This Many Children");
            child = levelSelect.transform.GetChild(i).gameObject;
            {
                GameObject temp = child.transform.GetChild(0).gameObject;
                if (temp.name == "Pass")
                {
                    switch (child.name)
                    {
                        case "Level 1":

                            if (levelCompletionCheck.getLevelStatus("Level 1"))
                            {
                                temp.SetActive(true);
                            }
                            break;
                        case "Level 2":

                            if (levelCompletionCheck.getLevelStatus("Level 2"))
                            {
                                temp.SetActive(true);
                            }
                            break;
                        case "Level 3":

                            if (levelCompletionCheck.getLevelStatus("Level 3"))
                            {
                                temp.SetActive(true);
                            }
                            break;

                    }
                }
            }
        }
        for (int y = 0, count = levelSelect.transform.childCount - 1; y < count; y++)
        {
            GameObject child = levelSelect.transform.GetChild(y).gameObject;
            {
                GameObject temp = child.transform.GetChild(1).gameObject;
                if (temp.name == "Lock")
                {
                    switch (child.name)
                    {
                        case "Level 2":

                            if (levelCompletionCheck.getLevelStatus("Level 1"))
                            {
                                temp.SetActive(false);
                            }
                            break;
                        case "Level 3":

                            if (levelCompletionCheck.getLevelStatus("Level 2"))
                            {
                                temp.SetActive(false);
                            }
                            break;

                    }
                }
            }
        }

        child = levelSelect.transform.GetChild(0).gameObject;
        Button button = levelSelect.transform.GetChild(0).gameObject.GetComponent<Button>();
        button.onClick.AddListener(loadLevel1);

        child = levelSelect.transform.GetChild(1).gameObject;
        button = levelSelect.transform.GetChild(1).gameObject.GetComponent<Button>();
        button.onClick.AddListener(loadLevel2);

        child = levelSelect.transform.GetChild(2).gameObject;
        button = levelSelect.transform.GetChild(2).gameObject.GetComponent<Button>();
        button.onClick.AddListener(loadLevel3);
    }
}

