using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Infrastructure.States
{
    public abstract class StateMachine<TMachineState> : IStateMachine<TMachineState> where TMachineState : class, IState
    {
        private readonly Dictionary<Type, TMachineState> _registeredStates = new();
        [CanBeNull] private IExitableState _currentState;

        public async UniTask Enter<TState>() where TState : class, TMachineState
        {
            var newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public void RegisterState<TState>(TState state) where TState : TMachineState
        {
            _registeredStates.Add(typeof(TState), state);
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, TMachineState
        {
            if (_currentState != null)
            {
                await _currentState.Exit();
            }

            var state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, TMachineState
        {
            return _registeredStates[typeof(TState)] as TState;
        }
    }
}