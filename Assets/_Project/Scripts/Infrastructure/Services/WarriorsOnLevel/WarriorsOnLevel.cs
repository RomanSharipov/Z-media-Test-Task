using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class WarriorsOnLevel : IWarriorsOnLevel
    {
        private List<Warrior> _botWarriors = new();
        private List<Warrior> _playerWarriors = new();
        private CompositeDisposable _disposables = new();

        private Subject<TeamType> _onTeamDefeated = new();
        private Subject<Unit> _onWarriorCountChanged = new();

        public IReadOnlyList<Warrior> BotWarriors => _botWarriors;
        public IReadOnlyList<Warrior> PlayerWarriors => _playerWarriors;
        public IObservable<TeamType> OnTeamDefeated => _onTeamDefeated;

        public IObservable<Unit> OnWarriorCountChanged => _onWarriorCountChanged;

        public void AddWarrior(Warrior warrior)
        {
            if (warrior.CurrentTeam == TeamType.Player)
            {
                _playerWarriors.Add(warrior);
                warrior.OnDied.Subscribe(_ => RemoveWarrior(warrior)).AddTo(_disposables);
            }
            else if (warrior.CurrentTeam == TeamType.Bot)
            {
                _botWarriors.Add(warrior);
                warrior.OnDied.Subscribe(_ => RemoveWarrior(warrior)).AddTo(_disposables);
            }
            _onWarriorCountChanged.OnNext(Unit.Default);
        }

        public void RemoveWarrior(Warrior warrior)
        {
            if (warrior.CurrentTeam == TeamType.Player)
            {
                _playerWarriors.Remove(warrior);

                if (_playerWarriors.Count == 0)
                    _onTeamDefeated.OnNext(TeamType.Player);
            }
            else if (warrior.CurrentTeam == TeamType.Bot)
            {
                _botWarriors.Remove(warrior);

                if (_botWarriors.Count == 0)
                    _onTeamDefeated.OnNext(TeamType.Bot);
            }
            _onWarriorCountChanged.OnNext(Unit.Default);
        }

        public void ClearTeam(TeamType team)
        {
            var list = team == TeamType.Player ? _playerWarriors : _botWarriors;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                var warrior = list[i];
                list.RemoveAt(i);

                if (warrior != null && warrior.gameObject != null)
                    UnityEngine.Object.Destroy(warrior.gameObject);
            }
        }

        public void ClearAll()
        {
            ClearTeam(TeamType.Player);
            ClearTeam(TeamType.Bot);
            _disposables.Clear();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}