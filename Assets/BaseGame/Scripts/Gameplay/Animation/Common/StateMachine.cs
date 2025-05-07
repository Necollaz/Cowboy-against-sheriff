using System.Collections.Generic;
using BaseGame.Scripts.Gameplay.Common.Enums;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class StateMachine
    {
        private readonly Dictionary<StateType, IState> _states;
        
        private IState _current;

        public StateMachine(Dictionary<StateType, IState> states)
        {
            _states = states;
        }

        public void ChangeState(StateType type)
        {
            _current?.Exit();
            _current = _states[type];
            _current.Enter();
        }

        public void Update()
        {
            _current?.HandleInput();
            _current?.LogicUpdate();
        }
    }
}