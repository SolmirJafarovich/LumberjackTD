using UnityEngine;

[System.Serializable]
public class DropEntry
{
    public GameObject prefab;
    [Range(0f, 1f)] public float dropChance = 1f;
    public Vector2Int quantityRange = new Vector2Int(1, 1);
}
