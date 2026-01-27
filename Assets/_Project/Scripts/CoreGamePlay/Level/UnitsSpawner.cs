using TriInspector;
using UnityEngine;
using VContainer;

namespace CodeBase.CoreGamePlay
{
    public class UnitsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _botSpawnPoint;
        [SerializeField] private int _unitsPerTeam = 20;
        [SerializeField] private float _spawnSpread = 5f;

        [Inject] private IUnitFactory _unitFactory;
        [Inject] private IUnitsOnLevel _unitsOnLevel;

        [Button]
        public void SpawnPlayerUnits()
        {
            SpawnTeam(_playerSpawnPoint, TeamType.Player);
        }
        [Button]
        public void SpawnBotUnits()
        {
            SpawnTeam(_botSpawnPoint, TeamType.Bot);
        }

        public void SpawnAll()
        {
            SpawnPlayerUnits();
            SpawnBotUnits();
        }

        private void SpawnTeam(Transform spawnPoint, TeamType team)
        {
            for (int i = 0; i < _unitsPerTeam; i++)
            {
                Vector3 position = GetRandomPosition(spawnPoint);
                Warrior unit = _unitFactory.CreateRandom(position, team);
                _unitsOnLevel.AddUnit(unit);
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