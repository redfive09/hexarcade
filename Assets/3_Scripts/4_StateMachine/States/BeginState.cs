using System.Collections;
using UnityEngine;

namespace _3_Scripts
{
    public class BeginState : MonoBehaviour, IState
    {
        public IEnumerator OnEnter()
        {
            print("Entering Begin State, Generating map");
            yield break;
        }

        public IEnumerator OnExit()
        {
            print("Exiting Begin State, Done Generating map");
            yield break;
        }

        public IEnumerator Run()
        {
            print("Running Begin State");
            yield break;
        }
    }
}