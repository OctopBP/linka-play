using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Infrastructure.States
{
    public abstract class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> registeredStates = new();
        [CanBeNull] private IExitableState currentState;

        public async UniTask Enter<TState>() where TState : class, IState
        {
            var newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            var newState = await ChangeState<TState>();
            await newState.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : class, IExitableState
        {
            registeredStates.Add(typeof(TState), state);
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (currentState != null)
            {
                await currentState.Exit();
            }

            var state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return registeredStates[typeof(TState)] as TState;
        }
    }
}