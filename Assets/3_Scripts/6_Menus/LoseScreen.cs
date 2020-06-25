using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{        
    [SerializeField] TextMeshProUGUI record;

    void Start()
    {
        SceneTransitionValues.lastMenuName = SceneManager.GetActiveScene().name;
        OneTimeLosingScreenEvents();

        if(SceneTransitionValues.record > 0)
        {
            record.text = "Record: " + Timer.GetTimeAsString(SceneTransitionValues.record, 3);
        }
        else
        {
            record.text = "No record, yet";
        }
    }

    private void OneTimeLosingScreenEvents()
    {
        if(!SceneTransitionValues.alreadyEnteredEndScreen)
        {
            GetComponent<AudioSource>().Play();
        }        
        SceneTransitionValues.alreadyEnteredEndScreen = true;
    }
}
