using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerField;

    private float startTime;
    private float finishTime;
    private float timeCounter;
    private float stopwatchCounter;
    private float stopwatchTime;
    private bool stopwatchMode = false;
    private bool timerIsRunning = true;

    private float bestTime;  // of current level
    private float[] bestTimes;

    
    public void GetReady()
    {
        bestTimes = SaveLoadManager.LoadTimes();       
        bestTime = bestTimes[SceneManager.GetActiveScene().buildIndex];
    }
    
    void FixedUpdate()
    {
        if(timerIsRunning)
        {
            if(stopwatchMode)
            {
                stopwatchCounter = stopwatchTime - Time.fixedTime;

                if(stopwatchCounter < 1)
                {
                    timerField.text = "GO!";               
                }
                else
                {
                    timerField.text = GetTimeAsString(stopwatchCounter, 1);                    
                }                
            }
            else
            {
                timeCounter = Time.fixedTime - startTime;
                timerField.text = GetTimeAsString(timeCounter, 2);
            }
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
        finishTime = timeCounter;
        timerIsRunning = false;
    }

    public float GetLastFinishTime()
    {
        return finishTime;
    }

    public void ShowLastFinishTime()
    {
        timerField.text = GetTimeAsString(finishTime, 3);
    }

    public float GetBestTime()
    {
        if(bestTime == 0)
        {            
            Debug.Log("This map has not been added to the 'Scenes In Build'");
        }
        
        return bestTime;
    }

    public float GetCurrentTime()
    {
        return timeCounter;
    }

    public void SetStopWatch(float seconds)
    {
        stopwatchTime = Time.fixedTime + seconds;
        stopwatchMode = true;
        timerIsRunning = true;
    }

    public bool IsStopTimeOver()
    {
        return  stopwatchTime <= Time.fixedTime;
    }

    public void Pause()
    {
        timerIsRunning = false;
    }
    
    public void Unpause()
    {
        startTime = Time.fixedTime - timeCounter; 
        timerIsRunning = true;
        stopwatchMode = false;
    }
    
    public bool IsNewBestTime()
    {
        if (CompareWithBestTime(finishTime))
        {
            bestTime = finishTime;
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

    public void TimeToTimerField(float time, int timerFormat)
    {
        int seconds = (int) time % 60;
        int minutes = (int) (time / 60);
        float milliseconds = time - (int) time;

        timerField.text = "";
        
        switch (timerFormat)
        {
            case 1:
                timerField.text += seconds.ToString();                
                break;
            
            case 2:
                int tenthsOfSecond = (int) (milliseconds * 10);
                timerField.text += minutes + ":" + (seconds.ToString("00")) + ":" + (tenthsOfSecond).ToString("0");
                break;

            case 3:
                int hundredthsOfseconds = (int) (milliseconds * 100);
                timerField.text += minutes + ":" + (seconds.ToString("00")) + ":" + (hundredthsOfseconds).ToString("00");
                break;
        }

        // if (timerFormat == 1)
        // {
        //     timerField.text += seconds.ToString();
        // }
        // else
        // {
        //     int minutes = (int) (time / 60);
        //     float milliseconds = time - (int) time;
            
        //     if(timerFormat == 2)
        //     {
        //         int tenthsOfSecond = (int) (milliseconds * 10);
        //         timerField.text += minutes + ":" + (seconds.ToString("00")) + ":" + (tenthsOfSecond).ToString("0");            
        //     }
        //     else
        //     {
        //         int hundredthsOfseconds = (int) (milliseconds * 100);
        //         timerField.text += minutes + ":" + (seconds.ToString("00")) + ":" + (hundredthsOfseconds).ToString("00");     
        //     }
        // }
    }

    public static string GetTimeAsString(float time, int timerFormat)
    {
        int seconds = (int) time % 60;
        int minutes = (int) (time / 60);
        float milliseconds = time - (int) time;
        
        switch (timerFormat)
        {
            case 1:
                return seconds.ToString();                
            
            case 2:
                int tenthsOfSecond = (int) (milliseconds * 10);
                return minutes + ":" + (seconds.ToString("00")) + ":" + (tenthsOfSecond).ToString("0");                

            case 3:
                int hundredthsOfseconds = (int) (milliseconds * 100);
                return minutes + ":" + (seconds.ToString("00")) + ":" + (hundredthsOfseconds).ToString("00");                
        }
        return "";
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
