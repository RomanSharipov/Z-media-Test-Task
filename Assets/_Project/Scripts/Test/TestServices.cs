using TriInspector;
using UnityEngine;
using VContainer;

public class TestServices : MonoBehaviour
{
    [Inject] IWarriorsOnLevel _warriorsOnLevel;

    [Button]
    public void Count()
    {
        Debug.Log($"BotWarriors = {_warriorsOnLevel.BotWarriors.Count} Player =  {_warriorsOnLevel.PlayerWarriors.Count}");
    }



}
