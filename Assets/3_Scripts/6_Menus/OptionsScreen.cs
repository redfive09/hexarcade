using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class OptionsScreen : MonoBehaviour
{
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject ResetTimesConfirmationScreen;
    [SerializeField] private GameObject TimesDeleted;

    public void ResetRecords()
    {
        Options.SetActive(false);
        ResetTimesConfirmationScreen.SetActive(true);
    }

    public void ConfirmReseting()
    {
        SaveLoadManager.ResetTimes();
        //Phone.Vibrate();
        TimesDeleted.SetActive(true);
        ResetTimesConfirmationScreen.SetActive(false);
    }
    public void CancelReseting()
    {
        Options.SetActive(true);
        ResetTimesConfirmationScreen.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}