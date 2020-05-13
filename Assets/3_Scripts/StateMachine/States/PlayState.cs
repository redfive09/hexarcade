namespace _3_Scripts
{
    internal class PlayState : State
    {
        public override string OnEnter()
        {
            return "entering play state";
        }

        public override string OnExit()
        {
            return "exiting play state";
        }

        public override string Run()
        {
            return "running play state";
        }
    }
}