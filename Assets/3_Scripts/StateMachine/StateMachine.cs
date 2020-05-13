using UnityEngine;

namespace _3_Scripts
{
    public class StateMachine : MonoBehaviour
    {
        private static StateMachine _sm;
        private States _currentState, _nextState;
        private Transitions _transition;
        private State beginState = new BeginState();
        private State playState = new PlayState();
        private State pauseState = new PauseState();
        private State loseState = new LoseState();

        public static StateMachine GetInstance()
        {
            if (_sm == null)
            {
                CreateInstance();
            }
            return _sm;
        }

        private static void CreateInstance()
        {
            var go = new GameObject();
            _sm = go.AddComponent<StateMachine>();
        }

        private void Start()
        {
            _sm = StateMachine.GetInstance();
            _currentState = States.Begin;
            _nextState = States.Play;
            _transition = Transitions.None;
            _currentState = States.Play;
            print(_currentState + " next state is " + _nextState);
        }

        private void FixedUpdate()
        {
            CheckTransition();

            CheckState();
            
            ExecuteState();
            
            _transition = Transitions.None;
        }

        private void CheckState()
        {
            if (_currentState == _nextState) return;
            OnStateChange();
            _currentState = _nextState;
        }

        private void CheckTransition()
        {
            if (_transition == Transitions.None) return;
            ChangeState();
        }

        private void OnStateChange()
        {
            switch (_currentState)
            {
                case States.Begin when _nextState == States.Play:
                    beginState.OnExit();
                    _currentState = _nextState;
                    playState.OnEnter();
                    break;
                case States.Play when _nextState == States.Pause:
                    playState.OnExit();
                    _currentState = _nextState;
                    pauseState.OnEnter();
                    break;
                case States.Pause when _nextState == States.Play:
                    pauseState.OnExit();
                    _currentState = _nextState;
                    playState.OnEnter();
                    break;
                case States.Pause when _nextState == States.Begin:
                    pauseState.OnExit();
                    _currentState = _nextState;
                    playState.OnEnter();
                    break;
                case States.Play when _nextState == States.Lose:
                    playState.OnExit();
                    _currentState = _nextState;
                    loseState.OnEnter();
                    break;
                case States.Lose when _nextState == States.Play:
                    loseState.OnExit();
                    _currentState = _nextState;
                    playState.OnEnter();
                    break;
            }
        }

        private void ExecuteState()
        {
            switch (_currentState)
            {
                case States.Begin :
                    beginState.Run();
                    break;
                case States.Play : 
                    playState.Run();
                    break;
                case States.Pause : 
                    pauseState.Run();
                    break;
                case States.Lose : 
                    loseState.Run();
                    break;
            }
        }

        private void ChangeState()
        {
            switch (_currentState)
            {
                case States.Begin when _transition == Transitions.Playpressed:
                    _nextState = States.Play;
                    break;
                case States.Play when _transition == Transitions.Pausepressed:
                    _nextState = States.Pause;
                    break;
                case States.Pause when _transition == Transitions.Resumepressed:
                    _nextState = States.Play;
                    break;
                case States.Pause when _transition == Transitions.Quitpressed:
                    _nextState = States.Begin;
                    break;
                case States.Play when _transition == Transitions.Levelup:
                    _nextState = States.Begin;
                    break;
                case States.Play when _transition == Transitions.Loseball:
                    _nextState = States.Lose;
                    break;
                case States.Lose when _transition == Transitions.Restartpressed:
                    _nextState = States.Begin;
                    break;
            }
        }

        public void Play()
        {
            _transition = Transitions.Playpressed;
        }

        public void Pause()
        {
            _transition = Transitions.Pausepressed;
        }

        public void Resume()
        {
            _transition = Transitions.Resumepressed;
        }

        public void Quit()
        {
            _transition = Transitions.Quitpressed;
        }

        public void Lose()
        {
            _transition = Transitions.Loseball;
        }

        public void Restart()
        {
           _transition = Transitions.Restartpressed;
        }

        public void LevelUp()
        {
            _transition = Transitions.Levelup;
        }
    }
}