using System;

namespace _3_Scripts
{
    class BeginState : State
    {
        public override string OnEnter()
        {
            return "entering begin state";
        }

        public override string OnExit()
        {
            return "exiting begin state";
        }

        public override String Run()
        {
            return "running begin state";
        }
    }
}