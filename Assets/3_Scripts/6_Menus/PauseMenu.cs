using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    /*
     * Tutorial used: https://youtu.be/JivuXdrIHK0
     */
    public bool GameIsCurrentlyPaused = false;
    public Button pauseButton;
    [SerializeField] GameObject pauseMenuUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeOrPause();
        }        
    }

    public void ResumeOrPause()
    {
        if (GameIsCurrentlyPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume() //public to be able to call it from the button
    {
        pauseMenuUI.SetActive(false); //disable Pause Menu (Child of the Canvas this script is linked to 
        Time.timeScale = 1f; // normal time
        GameIsCurrentlyPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); //enable Pause Menu (Child of the Canvas this script is linked to 
        Time.timeScale = 0f; // Stop time
        GameIsCurrentlyPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // normal time
        SceneManager.LoadScene(0);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); //Application.Quit does nothing visible, so I left the Debug.Log statement
        Application.Quit();
    }
}
