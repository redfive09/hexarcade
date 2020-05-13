using UnityEngine;

namespace _3_Scripts
{
    public class PauseState : State
    {
        public override string OnEnter()
        {
            Time.timeScale = 0;
            return "entering pause state";
        }

        public override string OnExit()
        {
            Time.timeScale = 1;
            return "exiting pause state";
        }

        public override string Run()
        {
            return "running pause state";
        }
    }
}