using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject spawnLocation;
    public float remainingTime;

    //public Object sceneToLoad;
    public GameObject failScreenUI;
    public GameObject passScreenUI;
    public levelCompletionCheck check;
    public gun gunScript;
    public GameObject gun;

    // Update is called once per frame

    private void Start()
    {
        gunScript = gun.GetComponent<gun>();
    }

    void Update()
    {
        //counts down timer
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            if (levelCompletionCheck.level1Check)
            {
                remainingTime = 0;

                LevelFailed();
            }

            if (!levelCompletionCheck.level1Check) {

                gunScript.Shoot();
            }


        }
        
        int seconds = Mathf.RoundToInt(remainingTime%60);
        int milliseconds = Mathf.RoundToInt(remainingTime * 100 % 1000);
        //timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        timerText.text = remainingTime.ToString("0.00");

    }

    public void RestartLevel()
    {

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("ppop");
        //Destroy(player);
        //RespawnPlayer();
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
        passScreenUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RespawnPlayer()
    {
        
    }


}
