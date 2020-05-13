using System.Collections;
using UnityEngine;

namespace _3_Scripts
{
    public class LoseState : MonoBehaviour, IState
    {
        
        //Change the time scale to 0, so that nothing moves, then destroy the ball
        public IEnumerator OnEnter()
        {
            print("Entering Lose State");
            Time.timeScale = 0;
            Destroy(GameObject.Find("Player1"));
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }

        public IEnumerator Run()
        {
            print("Running Lose State");
            yield break;
        }
    }
}