public interface IState
{
    void Enter();
    void Update();
    void Exit();
    void AddTransitions(params ITransition[] transitions);
}