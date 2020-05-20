using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // [SerializeField] private Text timerText;
    private float startTime;
    private float stopTime;
    private float timeCounter;

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
        this.timeCounter = Time.fixedTime - startTime;        
        
    }

    public float GetCurrentTime()
    {
        return timeCounter;
    }

    public void StartTiming() // = Reset timer
    {        
        startTime = Time.fixedTime;        
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

        return minutes.ToString() + ":" + (seconds.ToString("00")) + ":" + (miliseconds * 1000).ToString("000");
    }


} // END of class
