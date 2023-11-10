using System;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.SomeHelp;

public interface IState
{
    void OnEnter();
    void OnExit();
}

namespace Game.Core
{
    public abstract class BaseStateMachine<TState> : IDisposable where TState : IState
    {
        readonly Dictionary<Type, TState> states = new();
        protected Option<TState> maybeCurrentState;
        
        protected void AddState<TNewState>(TNewState state) where TNewState : TState =>
            states.Add(typeof(TNewState), state);
        public void EnterState<TNewState>() where TNewState: TState
        {
            var newState = states[typeof(TNewState)];
            EnterState(newState);
        }
		
        public void EnterState(TState newState)
        {
            maybeCurrentState.IfSome(currentState => currentState.OnExit());
            maybeCurrentState = newState.ToSome();
            newState.OnEnter();
        }

        public void Dispose() => maybeCurrentState.IfSome(currentState => currentState.OnExit());
    }
}