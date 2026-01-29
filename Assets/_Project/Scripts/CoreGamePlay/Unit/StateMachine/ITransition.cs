public interface ITransition
{
    public bool ShouldTransition();
    public IState TargetState { get; }
}