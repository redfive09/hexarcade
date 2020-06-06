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
    private float stopwatchCounter;
    private float stopwatchTime;
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
                stopwatchCounter = stopwatchTime - Time.fixedTime;

                if(stopwatchCounter < 0)
                {
                    stopwatchCounter = 0;
                }
                TimeToTimerField(stopwatchCounter, 1);
            }
            else
            {
                timeCounter = Time.fixedTime - startTime;
                TimeToTimerField(timeCounter, 2);
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
        stopTime = timeCounter;
        timerIsRunning = false;
    }

    public float GetLastFinishTime()
    {
        return stopTime;
    }

    public void ShowLastFinishTime()
    {
        TimeToTimerField(stopTime, 3);
    }

    public float GetBestTime()
    {
        return bestTime;
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

    public void TimeToTimerField(float time, int timerFormat)
    {
        int seconds = (int) time % 60;
        timerField.text = "";

        if (timerFormat == 1)
        {
            timerField.text += seconds.ToString();
        }
        else
        {
            int minutes = (int) (time / 60);
            float milliseconds = time - (int) time;
            
            if(timerFormat == 2)
            {
                int tenthsOfSecond = (int) (milliseconds * 10);
                timerField.text += minutes + ":" + (seconds.ToString("00")) + ":" + (tenthsOfSecond).ToString("0");            
            }
            else
            {
                int hundredthsOfseconds = (int) (milliseconds * 100);
                timerField.text += minutes + ":" + (seconds.ToString("00")) + ":" + (hundredthsOfseconds).ToString("00");     
            }
        }


  
        
        /*switch (timerFormat)
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
        }*/
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
