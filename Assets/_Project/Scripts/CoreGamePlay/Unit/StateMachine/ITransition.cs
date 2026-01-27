public interface ITransition
{
    bool ShouldTransition();
    IState TargetState { get; }
}