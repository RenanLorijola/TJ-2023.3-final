using System;

public interface IEventAction
{
    void DoEventAction(InteractibleEventAction action, Action<bool, InteractibleEventAction> completionCallback);
}