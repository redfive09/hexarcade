using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI record;
    [SerializeField] TextMeshProUGUI newRecord;

    void Start()
    {
        time.text += Timer.GetTimeAsString(SceneTransitionValues.time, 3);
        this.record.text += Timer.GetTimeAsString(SceneTransitionValues.record, 3);
    }
}
