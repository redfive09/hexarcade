using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    /*
     * Tutorial used: https://youtu.be/JivuXdrIHK0
     */
    public bool GameIsCurrentlyPaused ;
    public Button pauseButton;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] private int countDownTime; // int so that we dont display floting point numbers as count down
    [SerializeField] private TextMeshProUGUI CountDownDisplay;
    private Ball player;
    
    void Start()
    {
        player = GetComponentInParent<Ball>();
        GameIsCurrentlyPaused = false;
    }

    public void ResumeOrPause()
    {
        if (GameIsCurrentlyPaused)
        {
            player.GameUnpaused();
         //   StartCoroutine(CountDownToStart());
            Resume();
        }
        else
        {
            player.GamePaused();
            Pause();
        }
    }

    //we need to start the coroutine somewhere
    IEnumerator CountDownToStart()
    {
        CountDownDisplay.gameObject.SetActive(true);
        while (countDownTime > 0)
        {
            CountDownDisplay.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        CountDownDisplay.text = "GO!";
        // unpause game
        yield return new WaitForSeconds(1f);
        CountDownDisplay.gameObject.SetActive(false);
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
