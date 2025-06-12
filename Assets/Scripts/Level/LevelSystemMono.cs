using UnityEngine;

public class LevelSystemMono : MonoBehaviour
{
    [SerializeField] private GridSettings settings;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject stepMarkerPrefab;

    private LevelSystem levelSystem;

    void Start()
    {
        levelSystem = new LevelSystem(settings, nodePrefab, enemyPrefab, stepMarkerPrefab, transform);
    }

    void Update()
    {
        levelSystem?.Update();
    }
}
