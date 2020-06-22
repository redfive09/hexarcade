using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private int sceneIndex;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }

    public void StartGame()
    {
       SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1); // Last scene is the level selection screen
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneTransitionValues.currentScene);
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneTransitionValues.currentScene + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
