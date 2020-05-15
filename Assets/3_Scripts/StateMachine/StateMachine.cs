using UnityEngine;

namespace _3_Scripts
{
    public class StateMachine : MonoBehaviour
    {
        
        //Fields
        private static StateMachine _sm;
        private States _currentState, _nextState;
        private Transitions _transition;
        private IState _beginState;
        private IState _playState;
        private IState _pauseState;
        private IState _loseState;
        private IState _winState;

        /*
         *Give instance of State Machine
         * 
         *returns _sm - StateMachine Object
         */
        public static StateMachine GetInstance()
        {
            if (_sm == null)
            {
                CreateInstance();
            }
            return _sm;
        }

        /*
         * Create instance of State Machine
         */
        private static void CreateInstance()
        {
            var go = new GameObject();
            go.name = "StateMachine";
            _sm = go.AddComponent<StateMachine>();
        }

        /*
         * Initiate states from the field
         */
        private void initiateState()
        {
            var go = new GameObject();
            _beginState = go.AddComponent<BeginState>();
            _playState = go.AddComponent<PlayState>();
            _pauseState = go.AddComponent<PauseState>();
            _loseState = go.AddComponent<LoseState>();
            _winState = go.AddComponent<WinState>();
        }

        private void Start()
        {
            _sm = StateMachine.GetInstance();
            initiateState();
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

            _currentState = _nextState;
            _transition = Transitions.None;
        }
        
        /*
         * Checks if transition has changed or not
         * If transition is changed, change next state with ChangeState()
         */
        private void CheckTransition()
        {
            if (_transition == Transitions.None) return;
            _nextState = ChangeState();
        }
        
        /*
         * Checks if the current state is the same as next state
         * If not, call OnStateChange() which should be called, as soon as the state changes in a frame
         */
        private void CheckState()
        {
            if (_currentState == _nextState) return;
            OnStateChange();
            
        }

        /*
         * Run coroutine from each state when state is going to change
         * These coroutines are the ones to set up the running phase of a state or before the state is changing
         */
        private void OnStateChange()
        {
            switch (_currentState)
            {
                case States.Begin when _nextState == States.Play: // Start playing
                    StartCoroutine(_beginState.OnExit());
                    StartCoroutine(_playState.OnEnter());
                    break;

                case States.Play when _nextState == States.Pause: // Player pauses
                    StartCoroutine(_playState.OnExit());
                    StartCoroutine(_pauseState.OnEnter());
                    break;

                case States.Play when _nextState == States.Lose: // Player looses
                    StartCoroutine(_playState.OnExit());
                    StartCoroutine(_loseState.OnEnter());
                    break;

                case States.Play when _nextState == States.Win: // Player wins
                    StartCoroutine(_playState.OnExit());
                    StartCoroutine(_winState.OnEnter());
                    break;

                case States.Pause when _nextState == States.Play: // Player resumes
                    StartCoroutine(_pauseState.OnExit());
                    StartCoroutine(_playState.OnEnter());
                    break;

                case States.Pause when _nextState == States.Begin: // Player restarts by choice
                    StartCoroutine(_pauseState.OnExit());
                    StartCoroutine(_playState.OnEnter());
                    break;

                case States.Lose when _nextState == States.Begin: // Player restarts after loss
                    StartCoroutine(_loseState.OnExit());
                    StartCoroutine(_playState.OnEnter());
                    break;

                case States.Win when _nextState == States.Begin: // Game restarts by choice after winning
                    StartCoroutine(_winState.OnExit());
                    StartCoroutine(_beginState.OnEnter());
                    break;
            }
        }

        /*
         * Run the code from certain State
         */
        private void ExecuteState()
        {
            switch (_currentState)
            {
                case States.Begin :
                    _beginState.Run();
                    break;
                case States.Play : 
                    _playState.Run();
                    break;
                case States.Pause : 
                    _pauseState.Run();
                    break;
                case States.Lose : 
                    _loseState.Run();
                    break;
                case States.Win : 
                    _winState.Run();
                    break;
            }
        }

        /*
         * Checks if certain state and certain transition meets
         * 
         * returns States when condition met with certain mapping
         */
        private States ChangeState()
        {
            switch (_currentState)
            {
                case States.Begin when _transition == Transitions.Playpressed:
                    return States.Play;

                case States.Play when _transition == Transitions.Pausepressed:
                    return States.Pause;

                case States.Pause when _transition == Transitions.Resumepressed:
                    return States.Play;

                case States.Pause when _transition == Transitions.Quitpressed:
                    return States.Begin;

                case States.Play when _transition == Transitions.Levelup:
                    return States.Begin;

                case States.Play when _transition == Transitions.Loseball:
                    return States.Lose;

                case States.Lose when _transition == Transitions.Restartpressed:
                    return States.Begin;

                case States.Win when _transition == Transitions.Winball:
                    return States.Win;
            }
            return _currentState;
        }

        //======================Function to call when something happens in the game and make a transition=====================//
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

        public void Win()
        {
            _transition = Transitions.Winball;
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