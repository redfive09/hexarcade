using UnityEngine;

namespace _3_Scripts
{
    public class LoseState : State
    {
        public override string OnEnter()
        {
            Time.timeScale = 0;
            return "Entering lose state";
        }

        public override string OnExit()
        {
            Time.timeScale = 1;
            return "exiting lose state";
        }

        public override string Run()
        {
            
            return "running lose state";
        }
    }
}