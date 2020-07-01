using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private Dictionary<string, float> bestTimes;

    
    public void GetReady()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        if(bestTimes.TryGetValue(SceneTransitionValues.currentSceneName, out float bestTime))
        {
            this.bestTime = bestTime;
        }
        else
        {
            this.bestTime = float.MinValue;
        }
        
    }
    
    void FixedUpdate()
    {
        if(timerIsRunning)
        {
            if(stopwatchMode)
            {
                stopwatchCounter = stopwatchTime - Time.fixedTime;

                if(stopwatchCounter <= 0)
                {
                    timerField.text = "GO!";
                    // FadeOutInformation(2, 3, timerField);
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

    public bool IsStopwatchMode()
    {
        return stopwatchMode;
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
            bestTimes[SceneTransitionValues.currentSceneName] = bestTime;
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

        switch (timerFormat)
        {
            case 1:
            {               
                timerField.text = seconds.ToString();
                break;
            }
            
            case 2:
            {
                int tenthsOfSecond = (int) (milliseconds * 10);
                timerField.text = minutes + ":" + (seconds.ToString("00")) + ":" + (tenthsOfSecond).ToString("0");
                break;
            }

            case 3:
            {
                int hundredthsOfseconds = (int) (milliseconds * 100);
                timerField.text = minutes + ":" + (seconds.ToString("00")) + ":" + (hundredthsOfseconds).ToString("00");
                break;
            }
        }
    }

    public static string GetTimeAsString(float time, int timerFormat)
    {
        int seconds = (int) time % 60;
        int minutes = (int) (time / 60);
        float milliseconds = time - (int) time;
        
        switch (timerFormat)
        {
            case 1:
            {
                return (seconds+1).ToString();                
            }

            case 2:
            {
                int tenthsOfSecond = (int) (milliseconds * 10);
                return minutes + ":" + (seconds.ToString("00")) + ":" + (tenthsOfSecond).ToString("0");                
            }

            case 3:
            {
                int hundredthsOfseconds = (int) (milliseconds * 100);
                return minutes + ":" + (seconds.ToString("00")) + ":" + (hundredthsOfseconds).ToString("00");
            }
        }
        return "";
    }

    public static int ConvertToInt(float time)
    {
        // int newTime = (int) ((time - (int) time) * 100);        
        // newTime += (int) (time) * 100;
        // return newTime;
        return (int) (time * 100);
    }

    public static float ConvertToFloat(int time)
    {
        return (float) time / 100;
    }

    public static int GetSeconds(float time)
    {
        return (int) time;
    }

    public static int GetMilliseconds(float time)
    {
        float milliseconds = time - (int) time;
        return (int) (milliseconds * 100);
    }

    public static float GetTimeAsFloat(int seconds, int milliseconds)
    {
        float secondsFloat = (float) seconds + (float) milliseconds / 100;
        return secondsFloat;
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

       // Thx to --> https://stackoverflow.com/questions/56031067/using-coroutines-to-fade-in-out-textmeshpro-text-element
    private IEnumerator FadeOutInformation(float startFading, float fadingSpeed, TextMeshProUGUI text) 
    {         
         text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
         yield return new WaitForSeconds(startFading);
         while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * fadingSpeed));
            yield return null;
        }
    }
} // END of class
