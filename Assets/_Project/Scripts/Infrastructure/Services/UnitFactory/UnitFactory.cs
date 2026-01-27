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

    public Warrior Create(ShapeConfig shape, SizeConfig size, ColorConfig color, Vector3 position, TeamType team)
    {
        Warrior unit = Object.Instantiate(_database.UnitPrefab, position, Quaternion.identity, _sceneObjectsProvider.UnitsContainer);
        
        GameObject go = Object.Instantiate(shape.Prefab, position, Quaternion.identity, unit.transform);

        Renderer renderer = go.GetComponent<Renderer>();
        if (renderer == null)
            Debug.LogError($"[UnitFactory] '{shape.name}' prefab has NO Renderer. Add MeshRenderer.");

        Animator animator = go.GetComponent<Animator>();
        if (animator == null)
            Debug.LogError($"[UnitFactory] '{shape.name}' prefab has NO Animator. Add Animator component.");

        unit.View.Initialize(renderer, animator, color.Color, size.Scale);
        UnitData data = UnitDataBuilder.Build(_database.BaseStats, shape, size, color);
        unit.Initialize(data, team);
        
        return unit;
    }

    public Warrior CreateRandom(Vector3 position, TeamType team)
    {
        var shape = _database.Shapes[Random.Range(0, _database.Shapes.Length)];
        var size = _database.Sizes[Random.Range(0, _database.Sizes.Length)];
        var color = _database.Colors[Random.Range(0, _database.Colors.Length)];

        return Create(shape, size, color, position, team);
    }
}