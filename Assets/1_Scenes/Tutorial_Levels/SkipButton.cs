using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    public void SkipTutorial()
    {
        SceneManager.LoadScene("1_Scenes/Tutorial_Levels/Scene2");
    }
}
