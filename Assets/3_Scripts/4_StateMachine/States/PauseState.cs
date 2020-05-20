using System.Collections;
using UnityEngine;

namespace _3_Scripts
{
    public class PauseState: MonoBehaviour, IState
    {
        public IEnumerator OnEnter()
        {
            print("Entering Pause State");
            yield break;
        }

        public IEnumerator OnExit()
        {
            print("Exiting Pause State");
            yield break;
        }

        public IEnumerator Run()
        {
            print("Running Pause State");
            yield break;
        }
    }
}