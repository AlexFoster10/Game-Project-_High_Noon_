using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour

{
    public GameObject levelSelect;
    public GameObject homeScreen;
    public GameObject insrtructionsCanvas;
    public bool firstLaunch = true;
    public GameObject but1;
    public GameObject but2;
    public GameObject but3;
    public GameObject struct1;
    public GameObject struct2;
    public GameObject struct3;
    public GameObject struct4;
    GameObject child;
    private void Start()
    {
        if (gameObject.scene.name == "MainMenu")
        {
            levelSelect = GameObject.Find("Level Select");
            homeScreen = GameObject.Find("Home Screen");
            onStartDeactivate();
            levelIconCheck();
            levelSelect.SetActive(false);
        }





        if (gameObject.scene.name == "Level 1" && !levelCompletionCheck.getLevelStatus("Level 1")
            )
            {
            insrtructionsCanvas = GameObject.Find("Instructions");
            insrtructionsCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            insrtructionsCanvas.SetActive(true);
            struct1 = GameObject.Find("Struct1");
            struct2 = GameObject.Find("Struct2");
            struct3 = GameObject.Find("Struct3");
            struct4 = GameObject.Find("Struct4");
            struct2.SetActive(false);
            struct3.SetActive(false);
            struct4.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        if (gameObject.scene.name == "Level 2" && !levelCompletionCheck.getLevelStatus("Level 2")
            )
        {
            insrtructionsCanvas = GameObject.Find("Instructions");
            insrtructionsCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            insrtructionsCanvas.SetActive(true);
            struct3 = GameObject.Find("Struct3");
            struct4 = GameObject.Find("Struct4");
            struct4.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        if (gameObject.scene.name == "Level 1" && levelCompletionCheck.getLevelStatus("Level 1"))
        {
            insrtructionsCanvas = GameObject.Find("Instructions");
            insrtructionsCanvas.SetActive(false);
        }

        if (gameObject.scene.name == "Level 2" && levelCompletionCheck.getLevelStatus("Level 2"))
        {
            insrtructionsCanvas = GameObject.Find("Instructions");
            insrtructionsCanvas.SetActive(false);
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

    public void FirstButton()
    {
        struct1.SetActive(false);
        struct2.SetActive(true);
    }

    public void SecondButton()
    {
        struct2.SetActive(false);
        struct3.SetActive(true);
    }

    public void ThirdButton()
    {
        struct3.SetActive(false);
        struct4.SetActive(true);
    }

    public void FourthButton()
    {
        struct4.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
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
        Time.timeScale = 1f;
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

