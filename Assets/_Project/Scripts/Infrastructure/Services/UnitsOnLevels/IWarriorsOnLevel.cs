using CodeBase.CoreGamePlay;
using System;
using System.Collections.Generic;

public interface IWarriorsOnLevel
{
    public IReadOnlyList<Warrior> BotWarriors { get; }
    public IReadOnlyList<Warrior> PlayerWarriors { get; }
    public IObservable<TeamType> OnTeamDefeated { get; }
    public void AddWarrior(Warrior warrior);
    public List<Warrior> GetEnemies(TeamType team);
    public void RemoveWarrior(Warrior warrior);
}