using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{

    public TextMeshProUGUI nametext;
    public TextMeshProUGUI diatext;
    public Dialogue Mydialogue;

    public Queue<string> sentences;
    void Start()
    {
       
        sentences = new Queue<string>();
        StartDialogue(Mydialogue);
    }

    void Update()
    {
        Time.timeScale = 0f; // stop
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        //nametext.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue((sentence));
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        diatext.text = sentence;
    }
    
    

    public void EndDialogue()
    {
        Time.timeScale = 1f;
        Debug.Log("End");
    }

    
  
}
