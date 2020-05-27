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

    private float bestTime;  // of current level
    private float[] bestTimes;


    // Start is called before the first frame update
    void Start()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        bestTime = bestTimes[SceneManager.GetActiveScene().buildIndex];        
    }

    // Update is called once per frame
    void FixedUpdate()
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
        timerField.text = TimeToString(timeCounter);
    }


    public void StartTiming() // = Reset timer
    {        
        startTime = Time.fixedTime;
        stopwatchMode = false;
    }

    public void StopTiming()
    {
        stopTime = timeCounter;        
    }

    public float GetLastFinishTime()
    {
        return stopTime;
    }

    public float GetBestTime()
    {
        return bestTime;
    }

    public void SetStopWatch(float seconds)
    {
        stopWatchTime = Time.fixedTime + seconds;
        stopwatchMode = true;
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

    public string TimeToString(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)time % 60;
        float miliseconds = time - (int)time;

        return minutes.ToString() + ":" + (seconds.ToString("00")) + ":" + (miliseconds * 100).ToString("00");
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
