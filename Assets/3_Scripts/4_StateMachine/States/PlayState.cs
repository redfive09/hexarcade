using System.Collections;
using UnityEngine;

namespace _3_Scripts
{
    public class PlayState : MonoBehaviour, IState
    {
        public IEnumerator OnEnter()
        {
            print("Entering Play State");
            yield break;
        }

        public IEnumerator OnExit()
        {
            print("Exiting Play State");
            yield break;
        }

        public IEnumerator Run()
        {
            print("Running Play State");
            yield break;
        }
    }
}