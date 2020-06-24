
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    /*
     * Tutorial used: https://youtu.be/JivuXdrIHK0
     */    
    [SerializeField] GameObject pauseMenuUI;
    private Ball player;
    private bool GameIsCurrentlyPaused;
    
    void Start()
    {
        player = GetComponentInParent<Ball>();
        GameIsCurrentlyPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ResumeOrPause();
        }
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

    public void Resume() //public to be able to call it from the button
    {
        pauseMenuUI.SetActive(false); //disable Pause Menu (Child of the Canvas this script is linked to 
        Time.timeScale = 1f; // normal time
        GameIsCurrentlyPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); //enable Pause Menu (Child of the Canvas this script is linked to)
        Time.timeScale = 0f; // Stop time
        GameIsCurrentlyPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // normal time
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void SeeHighscores()
    {
        Time.timeScale = 1f;
        SceneTransitionValues.lastMenuName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("1_Scenes/_Menus/Highscores");
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); //Application.Quit does nothing visible, so I left the Debug.Log statement
        Application.Quit();
    }
}
