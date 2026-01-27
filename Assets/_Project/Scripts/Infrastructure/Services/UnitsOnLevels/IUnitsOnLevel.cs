using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;

public interface IUnitsOnLevel
{
    public IReadOnlyList<Warrior> BotUnits { get; }
    public IReadOnlyList<Warrior> PlayerUnits { get; }
    public IObservable<TeamType> OnTeamDefeated { get; }
    public void AddUnit(Warrior unit);
    public List<Warrior> GetEnemies(TeamType team);
    public void RemoveUnit(Warrior unit);
}