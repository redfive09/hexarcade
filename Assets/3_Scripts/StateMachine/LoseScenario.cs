using UnityEngine;

namespace _3_Scripts
{
    public class LoseScenario : MonoBehaviour
    {
        private StateMachine _sm;

        void Start()
        {
            _sm = StateMachine.GetInstance();
        }

        
        void Update()
        {
            //Change transition to loseball ball reached -10 in y axis
            if (!(gameObject.transform.localPosition.y <= -10)) return;
            _sm.Lose();
        }
    }
}
