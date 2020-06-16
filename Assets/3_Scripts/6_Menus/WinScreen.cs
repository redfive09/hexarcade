using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI record;
    [SerializeField] TextMeshProUGUI newRecord;

    void Start()
    {
        time.text += Timer.GetTimeAsString(WinScreenTimes.time, 3);
        this.record.text += Timer.GetTimeAsString(WinScreenTimes.record, 3);
    }
}
