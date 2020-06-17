using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI record;
    [SerializeField] TextMeshProUGUI newRecord;

    void Start()
    {
        if(SceneTransitionValues.newRecord)
        {
            newRecord.enabled = true;
            newRecord.gameObject.SetActive(true);
            newRecord.text += Timer.GetTimeAsString(SceneTransitionValues.time, 3);
        }
        else
        {
            time.enabled = true;
            time.gameObject.SetActive(true);
            time.text += Timer.GetTimeAsString(SceneTransitionValues.time, 3);

            this.record.enabled = true;
            this.record.gameObject.SetActive(true);
            this.record.text += Timer.GetTimeAsString(SceneTransitionValues.record, 3);
        }
    }
}