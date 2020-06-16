
using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointCounter : MonoBehaviour
{
    private TextMeshProUGUI counter;

    public int Counter
    {
        get
        {
            return Int32.Parse(Regex.Match(counter.text, @"\d+").Value);
        }
        set
        {
            counter.text = String.Format("{0,2} left",  value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        counter = GetComponent<TextMeshProUGUI>();
    }

    //Update is called once per frame
    //void Update()
    //{

    //    Counter = 12;
    //    Debug.Log(Counter);
    //}
}
