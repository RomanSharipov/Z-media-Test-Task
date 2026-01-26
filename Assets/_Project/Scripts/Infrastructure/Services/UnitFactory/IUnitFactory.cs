using CodeBase.CoreGamePlay;
using UnityEngine;

public interface IUnitFactory
{
    public Unit Create(ShapeConfig shape, SizeConfig size, ColorConfig color, Vector3 position);
    public Unit CreateRandom(Vector3 position);
}