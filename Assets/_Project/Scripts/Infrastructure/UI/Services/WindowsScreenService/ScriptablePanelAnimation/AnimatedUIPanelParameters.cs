using UiScenes;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatedUIPanelParameters", menuName = "UiScenes/AnimatedUIPanelParameters")]
public class AnimatedUIPanelParameters : ScriptableObject
{
    [SerializeField]
    private int _backgroundSortOrder;
    [SerializeField]
    private int _foregroundSortOrder;
    [SerializeField]
    private ScriptableUIAnimation _animation;

    public int BackgroundSortOrder => _backgroundSortOrder;
    public int ForegroundSortOrder => _foregroundSortOrder;
    public ScriptableUIAnimation Animation => _animation;
}