public class WarriorStateMachine
{
    private IState _currentState;

    public IState CurrentState => _currentState;

    public void SetState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}