using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _activeState;

        public IState ActiveState => _activeState;

        public void AddState(Type type, IState state)
        {
            _states.Add(type, state);
        }

        public async UniTask Enter<TState>() where TState : IState
        {
            if (_activeState?.GetType() == typeof(TState))
            {
                Debug.LogError($"State {typeof(TState).Name} is already active. activeState = {_activeState?.GetType()}");
                return;
            }

            await ExitActiveState();

            IState state = _states[typeof(TState)];
            _activeState = state;
            Debug.Log($"Go to {typeof(TState).Name}");
            await state.Enter();
        }
        
        public async UniTask ExitActiveState()
        {
            if (_activeState != null)
            {
                await _activeState.Exit();
                _activeState = null;
            }
        }
    }
}
