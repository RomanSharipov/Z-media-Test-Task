using CodeBase.CoreGamePlay;
using UnityEngine;

public interface IUnitFactory
{
    public Warrior Create(ShapeConfig shape, SizeConfig size, ColorConfig color, Vector3 position, TeamType team);
    public Warrior CreateRandom(Vector3 position, TeamType team);
}