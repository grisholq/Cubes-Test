using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class StateMachine : IStateMachine, ITickable, IFixedTickable
{
    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
    private IState _currentState;
    private List<StateMachineTransition> _transitions = new List<StateMachineTransition>();

    public void AddState<T>(T state) where T : IState
    {
        _states.Add(typeof(T), state);
        state.SetStateMachine(this);
    }
    
    public void SwitchState<T>()
    {
        _currentState?.Exit();
        _currentState = _states[typeof(T)];
        _currentState.Enter();
    }

    public void AddTransition<TFrom, TTo>(Func<bool> condition)
        where TFrom : IState
        where TTo : IState
    {
        _transitions.Add(new StateMachineTransition(typeof(TFrom), typeof(TTo), condition));
    }

    public void Tick()
    {
        CheckTransitions();
        _currentState?.Update();
    }

    public void FixedTick()
    {
        _currentState?.FixedUpdate();
    }

    private void CheckTransitions()
    {
        foreach (var transition in _transitions)
        {
            if (transition.From == _currentState.GetType() && transition.Condition())
            {
                SwitchState(transition.To);
                return;
            }
        }
    }

    private void SwitchState(Type stateType)
    {
        _currentState?.Exit();
        _currentState = _states[stateType];
        _currentState.Enter();
    }
}
