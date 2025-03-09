using System;

public class StateMachineTransition
{
    public Type From { get; }
    public Type To { get; }
    public Func<bool> Condition { get; }

    public StateMachineTransition(Type from, Type to, Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}