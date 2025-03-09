using System;

public interface IStateMachine
{
     void AddState<T>(T state) where T : IState;
     void SwitchState<T>();
}
