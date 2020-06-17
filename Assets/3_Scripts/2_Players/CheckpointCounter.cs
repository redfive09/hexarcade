using System;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class CheckpointCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;

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

    public void SetCounter(int number)
    {
        counter.text = number.ToString();
    }

    public void Appear()
    {     
        counter.enabled = true;
        counter.gameObject.SetActive(true);
    }

    public void Disappear()
    {     
        counter.enabled = false;
        counter.gameObject.SetActive(false);
    }
}
