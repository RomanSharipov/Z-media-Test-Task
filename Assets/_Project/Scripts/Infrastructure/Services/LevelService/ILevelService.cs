using CodeBase.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface ILevelService
    {
        public UniTask<ISceneInitializer> LoadCurrentLevel();
        public void UnLoadCurrentLevel();
    }
}