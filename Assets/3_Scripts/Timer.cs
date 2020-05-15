using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // [SerializeField] private Text timerText;
    private float startTime;
    private float timeCounter;

    // Start is called before the first frame update
    private void Start()
    {
        startTime = Time.fixedTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timeCounter = Time.fixedTime - startTime;
        // timerText.text = TimeToString(timeCounter);
    }

    public static string TimeToString(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)time % 60;
        float miliseconds = time - (int)time;

        return minutes.ToString() + ":" + (seconds.ToString("00")) + ":" + (miliseconds * 1000).ToString("000");
    }

    public float GetCurrentTime()
    {
        return timeCounter;
    }

    public void ResetTime()
    {
        startTime = Time.fixedTime;
    }

    // public void StopTimer()
    // {
    //     timerText.gameObject.SetActive(false);
    // }


} // END of class
