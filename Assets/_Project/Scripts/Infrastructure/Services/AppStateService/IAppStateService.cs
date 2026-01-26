using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface IAppStateService
    {
        void AddState(IState state);
        UniTask Enter<TState>() where TState : IState;
    }
}
