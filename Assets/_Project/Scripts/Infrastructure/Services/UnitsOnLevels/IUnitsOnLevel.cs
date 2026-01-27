using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;

public interface IUnitsOnLevel
{
    IReadOnlyList<Warrior> BotUnits { get; }
    IReadOnlyList<Warrior> PlayerUnits { get; }
    IObservable<TeamType> OnTeamDefeated { get; }

    void AddUnit(Warrior unit);
    List<Warrior> GetEnemies(TeamType team);
    void RemoveUnit(Warrior unit);
}