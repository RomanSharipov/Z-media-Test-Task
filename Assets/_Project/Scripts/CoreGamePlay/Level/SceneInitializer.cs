using UnityEngine;
using VContainer;

namespace CodeBase.CoreGamePlay
{
    public class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        [SerializeField] private Transform _unitsContainer;
        [SerializeField] private WarriorsSpawner _warriorsSpawner;

        [Inject] private ISceneObjectsProvider _sceneObjectsProvider;

        public void InitializeSceneServices()
        {
            _sceneObjectsProvider.UnitsContainer = _unitsContainer;
            _sceneObjectsProvider.WarriorsSpawner = _warriorsSpawner;
        }
    }
}
