using UnityEngine;

public class PlayerSpawner
{
    private readonly GridFactory gridFactory;
    private readonly GameObject playerPrefab;
    private readonly LevelObjectSpawner objectSpawner;
    private GameObject currentPlayer;

    public PlayerSpawner(GridFactory gridFactory, GameObject playerPrefab, LevelObjectSpawner objectSpawner)
    {
        this.gridFactory = gridFactory;
        this.playerPrefab = playerPrefab;
        this.objectSpawner = objectSpawner;
    }

    public void SpawnPlayer(Vector2Int spawnPosition)
    {
        if (currentPlayer != null)
        {
            Object.Destroy(currentPlayer);
        }

        Vector3 worldPosition = new Vector3(spawnPosition.x, 1f, spawnPosition.y);
        currentPlayer = Object.Instantiate(playerPrefab, worldPosition, Quaternion.identity);

        var interaction = currentPlayer.GetComponent<PlayerInteraction>();
        if (interaction != null)
        {
            interaction.Init(gridFactory, objectSpawner);
        }
        else
        {
            Debug.LogWarning("Player prefab missing PlayerInteraction component!");
        }
    }

}
