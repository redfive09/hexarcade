using UnityEngine;
using TMPro;

public class LoseScreen : MonoBehaviour
{        
    [SerializeField] TextMeshProUGUI record;    

    void Start()
    {
        if(SceneTransitionValues.record > 0)
        {
            record.text = "Record: " + Timer.GetTimeAsString(SceneTransitionValues.record, 3);
        }
        else
        {
            record.text = "No record, yet";
        }
    }
}
