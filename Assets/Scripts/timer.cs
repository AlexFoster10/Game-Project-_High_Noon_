using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Timeline;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;


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
        }
        
        int seconds = Mathf.RoundToInt(remainingTime%60);
        int milliseconds = Mathf.RoundToInt(remainingTime * 100 % 1000);
        timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        //timerText.text = remainingTime.ToString("0.00");

    }
}
