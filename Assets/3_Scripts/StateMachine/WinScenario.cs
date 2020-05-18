using _3_Scripts;
using UnityEngine;

public class WinScenario : MonoBehaviour
{
    private StateMachine _sm;
    // Start is called before the first frame update
    void Start()
    {
        _sm = StateMachine.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!(gameObject.GetComponent<Hexagon>().IsWinningTile())) return;
        print("touched winning tile");
        _sm.LevelUp();
    }
}