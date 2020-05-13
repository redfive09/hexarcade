using UnityEngine;

namespace _3_Scripts
{
    public class LoseGame : MonoBehaviour
    {
        private StateMachine _sm;

        void Start()
        {
            _sm = StateMachine.GetInstance();
        }

        void Update()
        {
            if (!(gameObject.transform.localPosition.y <= -10)) return;
            _sm.Lose();
            Destroy(gameObject);
        }
    }
}
