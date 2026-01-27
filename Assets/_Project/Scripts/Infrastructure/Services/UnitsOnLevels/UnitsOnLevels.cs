using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;
using UniRx;

public class WarriorsOnLevel : IWarriorsOnLevel
{
    private List<Warrior> _botUnits = new();
    private List<Warrior> _playerUnits = new();
    private CompositeDisposable _disposables = new();

    public IReadOnlyList<Warrior> BotWarriors => _botUnits;
    public IReadOnlyList<Warrior> PlayerWarriors => _playerUnits;

    private Subject<TeamType> _onTeamDefeated = new();
    public IObservable<TeamType> OnTeamDefeated => _onTeamDefeated;

    public void AddWarrior(Warrior unit)
    {
        if (unit.CurrentTeam == TeamType.Player)
        {
            _playerUnits.Add(unit);
            unit.OnDied.Subscribe(_ => RemoveWarrior(unit)).AddTo(_disposables);
        }
        else if (unit.CurrentTeam == TeamType.Bot)
        {
            _botUnits.Add(unit);
            unit.OnDied.Subscribe(_ => RemoveWarrior(unit)).AddTo(_disposables);
        }
    }

    public void RemoveWarrior(Warrior unit)
    {
        if (unit.CurrentTeam == TeamType.Player)
        {
            _playerUnits.Remove(unit);

            if (_playerUnits.Count == 0)
                _onTeamDefeated.OnNext(TeamType.Player);
        }
        else if (unit.CurrentTeam == TeamType.Bot)
        {
            _botUnits.Remove(unit);

            if (_botUnits.Count == 0)
                _onTeamDefeated.OnNext(TeamType.Bot);
        }
    }

    public List<Warrior> GetEnemies(TeamType team)
    {
        return team == TeamType.Player
            ? _botUnits
            : _playerUnits;
    }

    public void Clear()
    {
        _disposables.Clear();
        _playerUnits.Clear();
        _botUnits.Clear();
    }
}