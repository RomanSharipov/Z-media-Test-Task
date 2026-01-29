using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;
using UniRx;

public interface IWarriorsOnLevel
{
    IReadOnlyList<Warrior> BotWarriors { get; }
    IReadOnlyList<Warrior> PlayerWarriors { get; }
    IObservable<TeamType> OnTeamDefeated { get; }
    IObservable<Unit> OnWarriorCountChanged { get; }

    void AddWarrior(Warrior warrior);
    void RemoveWarrior(Warrior warrior);
    void ClearTeam(TeamType team);
    void ClearAll();
}