namespace _3_Scripts
{
    public abstract class State
    {
        public abstract string OnEnter();
        public abstract string OnExit();
        public abstract string Run();
    }
}