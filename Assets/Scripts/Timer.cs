using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float totalTime = 60.0f; // Total time in seconds
    private float elapsedTime = 0.0f;
    private bool isTimerRunning = true;

    void Update()
    {
        if (isTimerRunning)
        {
            // Increment the elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Update the timer text to display the remaining time
            float remainingTime = totalTime - elapsedTime;
            if (remainingTime > 0)
            {
                // Format the remaining time as minutes and seconds
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

                // Update the timer text
                timerText.text = timeString;
            }
            else
            {
                // Timer has reached zero, stop the timer
                isTimerRunning = false;
                GameManager.Instance.CheckGameResult();
            }
        }
    } 
}
