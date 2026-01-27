using CodeBase.CoreGamePlay;
using System.Collections.Generic;
using System.Threading;

public abstract class StateBase : IState
{
    private List<ITransition> _transitions = new List<ITransition>();

    protected Warrior _warrior;

    public StateBase(Warrior warrior)
    {
        _warrior = warrior;
    }

    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();

    public void Update()
    {
        UpdateState();
        HandleTransitions();
    }

    public void AddTransitions(params ITransition[] transitions)
    {
        foreach (ITransition transition in transitions)
        {
            _transitions.Add(transition);
        }
    }

    private void HandleTransitions()
    {
        foreach (ITransition transition in _transitions)
        {
            if (transition.ShouldTransition())
            {
                _warrior.StateMachine.SetState(transition.TargetState);
                return;
            }
        }
    }
}