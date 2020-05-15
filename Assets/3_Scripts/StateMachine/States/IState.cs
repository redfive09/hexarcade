using System.Collections;

namespace _3_Scripts
{
    public interface IState
    {
        IEnumerator OnEnter();
        IEnumerator OnExit();
        IEnumerator Run();
    }
}