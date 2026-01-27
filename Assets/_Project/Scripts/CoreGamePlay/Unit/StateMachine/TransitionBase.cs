using CodeBase.CoreGamePlay;

public abstract class TransitionBase : ITransition
{
    protected Warrior _warrior;
    private IState _targetState;

    public IState TargetState => _targetState;

    public abstract bool ShouldTransition();

    public TransitionBase(Warrior warrior, IState targetState)
    {
        _warrior = warrior;
        _targetState = targetState;
    }
}