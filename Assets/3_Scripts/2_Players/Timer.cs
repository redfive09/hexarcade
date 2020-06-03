using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerField;

    private float startTime;
    private float stopTime;
    private float timeCounter;
    private float stopWatchTime;
    private bool stopwatchMode = false;
    private bool timerIsRunning = true;

    private float bestTime;  // of current level
    private float[] bestTimes;


    // Start is called before the first frame update
    void Start()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        // bestTime = bestTimes[SceneManager.GetActiveScene().buildIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timerIsRunning)
        {
            if(stopwatchMode)
            {
                timeCounter = stopWatchTime - Time.fixedTime;

                if(timeCounter < 0)
                {
                    timeCounter = 0;
                }
            }
            else
            {
                timeCounter = Time.fixedTime - startTime;
            }
            TimeToTimerField(timeCounter, false);
        }
    }


    public void StartTiming() // = Reset timer
    {        
        startTime = Time.fixedTime;
        stopwatchMode = false;
        timerIsRunning = true;
    }

    public void StopTiming()
    {
        stopTime = timeCounter;
        timerIsRunning = false;
    }

    public float GetLastFinishTime()
    {
        return stopTime;
    }

    public void ShowLastFinishTime()
    {
        TimeToTimerField(stopTime, true);
    }

    public float GetBestTime()
    {
        return bestTime;
    }

    public void SetStopWatch(float seconds)
    {
        stopWatchTime = Time.fixedTime + seconds;
        stopwatchMode = true;
        timerIsRunning = true;
    }

    public bool IsStopTimeOver()
    {
        return  stopWatchTime <= Time.fixedTime;
    }

    public bool IsNewBestTime()
    {
        if (CompareWithBestTime(stopTime))
        {
            bestTime = stopTime;
            bestTimes[SceneManager.GetActiveScene().buildIndex] = bestTime;
            SaveLoadManager.SaveTimes(bestTimes);
            return true;
        }
        return false;
    }

    private bool CompareWithBestTime(float newTime)
    {
        // best times of not yet cleared levels are negative floats
        if (bestTime < 0.0f || newTime < bestTime)
        {
            return true;
        }
        return false;
    }

    public void TimeToTimerField(float time, bool displayFullTime)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)time % 60;
        float miliseconds = time - (int)time;        
        
        timerField.text = minutes.ToString() + ":" + (seconds.ToString("00")) + ":";

        if(displayFullTime)
        {
            int hundredthsOfseconds = (int) (miliseconds * 100);            
            timerField.text += (hundredthsOfseconds).ToString("00");
        }
        else
        {
            int tenthsOfSecond = (int) (miliseconds * 10);
            timerField.text += (tenthsOfSecond).ToString("0");
        }        
    }
    
    public void Show()
    {
        timerField.enabled = true;
        timerField.gameObject.SetActive(true);        
    }

    public void Disappear()
    {
        timerField.enabled = false;
        timerField.gameObject.SetActive(false);
    }

} // END of class
