namespace _3_Scripts
{
    public class LevelUp :  State
    {
        public override string OnEnter()
        {
            return "entering level up state";
        }

        public override string OnExit()
        {
            return "exiting level up state";
        }

        public override string Run()
        {
            return "running level up state";
        }
    }
}