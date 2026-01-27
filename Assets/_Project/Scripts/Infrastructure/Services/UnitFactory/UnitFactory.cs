using CodeBase.CoreGamePlay;
using UnityEngine;

public class UnitFactory : IUnitFactory
{
    private readonly UnitConfigDatabase _database;
    private readonly ISceneObjectsProvider _sceneObjectsProvider;

    public UnitFactory(UnitConfigDatabase database, ISceneObjectsProvider sceneObjectsProvider)
    {
        _database = database;
        _sceneObjectsProvider = sceneObjectsProvider;
    }

    public Unit Create(ShapeConfig shape, SizeConfig size, ColorConfig color, Vector3 position)
    {
        Unit unit = Object.Instantiate(_database.UnitPrefab, position, Quaternion.identity, _sceneObjectsProvider.UnitsContainer);
        
        UnitView unitView = Object.Instantiate(shape.UnitViewPrefab, position, Quaternion.identity, _sceneObjectsProvider.UnitsContainer);

        

        unitView.transform.SetParent(unit.transform);

        UnitData data = UnitDataBuilder.Build(_database.BaseStats, shape, size, color);
        unit.Initialize(data, color.Color, size.Scale, unitView);
        return unit;
    }

    public Unit CreateRandom(Vector3 position)
    {
        var shape = _database.Shapes[Random.Range(0, _database.Shapes.Length)];
        var size = _database.Sizes[Random.Range(0, _database.Sizes.Length)];
        var color = _database.Colors[Random.Range(0, _database.Colors.Length)];

        return Create(shape, size, color, position);
    }
}