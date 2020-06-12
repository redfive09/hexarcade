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
       // SceneManager.LoadScene(1);
        SceneManager.LoadScene("1_Scenes/Tutorial_Levels/Scene1");

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    
    
    public void Restart()
    {
        // Scene scene = SceneManager.GetActiveScene(); 
        // SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene(sceneIndex - 1);
    }

    public void Next()
    {
        // Scene scene = SceneManager.GetActiveScene(); 
        // SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
