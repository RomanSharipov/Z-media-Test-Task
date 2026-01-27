using CodeBase.CoreGamePlay;
using UnityEngine;

public interface IWarriorFactory
{
    public Warrior Create(ShapeConfig shape, SizeConfig size, ColorConfig color, Vector3 position, TeamType team);
    public Warrior CreateRandom(Vector3 position, TeamType team);
}