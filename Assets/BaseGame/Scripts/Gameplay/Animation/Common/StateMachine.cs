using System.Collections.Generic;
using BaseGame.Scripts.Gameplay.Enums;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class StateMachine
    {
        private readonly Dictionary<StateType, State> _states;
        
        private State _current;

        public StateMachine(Dictionary<StateType, State> states)
        {
            _states = states;
        }

        public void ChangeState(StateType type)
        {
            _current?.Exit();
            _current = _states[type];
            _current.Enter();
        }

        public void UpdateState()
        {
            _current?.HandleInput();
            _current?.LogicUpdate();
        }
    }
}