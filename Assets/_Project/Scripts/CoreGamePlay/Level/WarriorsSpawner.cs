using TriInspector;
using UnityEngine;
using VContainer;

namespace CodeBase.CoreGamePlay
{
    public class WarriorsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _botSpawnPoint;
        [SerializeField] private int _unitsPerTeam = 20;
        [SerializeField] private float _spawnSpread = 5f;

        [Inject] private IWarriorFactory _unitFactory;
        [Inject] private IWarriorsOnLevel _warriorsOnLevel;

        [Button]
        public void SpawnAll()
        {
            SpawnTeam(_playerSpawnPoint, TeamType.Player);
            SpawnTeam(_botSpawnPoint, TeamType.Bot);
        }

        [Button]
        public void RandomizeArmies()
        {
            _warriorsOnLevel.ClearAll();
            SpawnAll();
        }

        [Button]
        public void RandomizePlayerArmy()
        {
            _warriorsOnLevel.ClearTeam(TeamType.Player);
            SpawnTeam(_playerSpawnPoint, TeamType.Player);
        }

        [Button]
        public void RandomizeBotArmy()
        {
            _warriorsOnLevel.ClearTeam(TeamType.Bot);
            SpawnTeam(_botSpawnPoint, TeamType.Bot);
        }

        private void SpawnTeam(Transform spawnPoint, TeamType team)
        {
            for (int i = 0; i < _unitsPerTeam; i++)
            {
                Vector3 position = GetRandomPosition(spawnPoint);
                Warrior warrior = _unitFactory.CreateRandom(position, team);
                _warriorsOnLevel.AddWarrior(warrior);
            }
        }

        private Vector3 GetRandomPosition(Transform spawnPoint)
        {
            Vector3 offset = new Vector3(
                Random.Range(-_spawnSpread, _spawnSpread),
                0f,
                Random.Range(-_spawnSpread, _spawnSpread)
            );
            return spawnPoint.position + offset;
        }
    }
}