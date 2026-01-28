using UnityEngine;
using VContainer;

namespace CodeBase.CoreGamePlay
{
    public class WarriorsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _botSpawnPoint;

        [Inject] private IWarriorFactory _unitFactory;
        [Inject] private IWarriorsOnLevel _warriorsOnLevel;
        [Inject] private BattleConfig _battleConfig;

        private int _unitsPerTeam;
        private float _spawnSpread;


        private void Awake()
        {
            _unitsPerTeam = _battleConfig.UnitsPerTeam;
            _spawnSpread = _battleConfig.SpawnSpread;
        }

        public void SpawnAll()
        {
            SpawnTeam(_playerSpawnPoint, TeamType.Player);
            SpawnTeam(_botSpawnPoint, TeamType.Bot);
        }


        public void RandomizeArmies()
        {
            _warriorsOnLevel.ClearAll();
            SpawnAll();
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

            Vector3 position = spawnPoint.position + offset;

            if (Physics.Raycast(position + Vector3.up * 5f, Vector3.down, out var hit, 10f))
                position.y = hit.point.y;

            return position;
        }
    }
}