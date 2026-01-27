using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;
using UniRx;

public class UnitsOnLevel : IUnitsOnLevel
{
    private List<Warrior> _botUnits = new();
    private List<Warrior> _playerUnits = new();
    private CompositeDisposable _disposables = new();

    public IReadOnlyList<Warrior> BotUnits => _botUnits;
    public IReadOnlyList<Warrior> PlayerUnits => _playerUnits;

    private Subject<TeamType> _onTeamDefeated = new();
    public IObservable<TeamType> OnTeamDefeated => _onTeamDefeated;

    public void AddUnit(Warrior unit)
    {
        if (unit.CurrentTeam == TeamType.Player)
        {
            _playerUnits.Add(unit);
            unit.Died.Subscribe(_ => RemoveUnit(unit)).AddTo(_disposables);
        }
        else if (unit.CurrentTeam == TeamType.Bot)
        {
            _botUnits.Add(unit);
            unit.Died.Subscribe(_ => RemoveUnit(unit)).AddTo(_disposables);
        }
    }

    public void RemoveUnit(Warrior unit)
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