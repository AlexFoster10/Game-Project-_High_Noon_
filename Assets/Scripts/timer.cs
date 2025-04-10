using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public Object sceneToLoad;
    public GameObject failScreenUI;
    public GameObject passScreenUI;



    // Update is called once per frame
    void Update()
    {

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;

            LevelFailed();
        }
        
        int seconds = Mathf.RoundToInt(remainingTime%60);
        int milliseconds = Mathf.RoundToInt(remainingTime * 100 % 1000);
        //timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        timerText.text = remainingTime.ToString("0.00");

    }

    public void RestartLevel()
    {
        
        failScreenUI.SetActive(false);
        passScreenUI.SetActive(false);
        SceneManager.LoadScene(gameObject.scene.name);
        Time.timeScale = 1f;
    }

    public void LevelFailed()
    {
        failScreenUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LevelPassed()
    {
        failScreenUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
