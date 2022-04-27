using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ClockScript : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TMP_Text timeText;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {

                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            
        }
    }
    public void subtractTime(float time)
    {
        timeRemaining -= time;
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay / 60);
        if (minutes < 0)
        {
            minutes = 0f;
        }
        timeText.text = string.Format("{1:00}", minutes, seconds);
        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
