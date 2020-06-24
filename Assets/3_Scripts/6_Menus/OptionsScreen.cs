using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class OptionsScreen : MonoBehaviour
{

    public void ResetRecords()
    {
        SaveLoadManager.ResetTimes();        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}