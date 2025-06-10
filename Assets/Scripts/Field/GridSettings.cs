using UnityEngine;

[CreateAssetMenu(menuName = "Game/GridSettings")]
public class GridSettings : ScriptableObject
{
    public int Width = 10;
    public int Height = 10;
    [Range(0f, 1f)] public float BlockedChance = 0.2f;
}
