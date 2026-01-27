using TriInspector;
using UnityEngine;
using VContainer;

public class TestServices : MonoBehaviour
{
    [Inject] IUnitFactory _unitFactory;

    [Button]
    public void CreateRandom()
    {
        _unitFactory.CreateRandom(Vector3.zero);
    }

}
