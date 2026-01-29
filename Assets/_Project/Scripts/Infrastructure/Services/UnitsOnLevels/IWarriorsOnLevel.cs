using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;
using UniRx;

public interface IWarriorsOnLevel
{
    public IReadOnlyList<Warrior> BotWarriors { get; }
    public IReadOnlyList<Warrior> PlayerWarriors { get; }
    public IObservable<TeamType> OnTeamDefeated { get; }
    public IObservable<Unit> OnWarriorCountChanged { get; }
     
    public void AddWarrior(Warrior warrior);
    public void RemoveWarrior(Warrior warrior);
    public void ClearTeam(TeamType team);
    public void ClearAll();
}