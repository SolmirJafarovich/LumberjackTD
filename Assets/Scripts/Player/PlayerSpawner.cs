using UnityEngine;

public class PlayerSpawner
{
    private readonly GridFactory gridFactory;
    private readonly GameObject playerPrefab;
    private readonly UIRingController ringUI;
    private readonly LevelObjectSpawner objectSpawner;
    private readonly PlayerHealthUIBinder playerHealthUIBinder;
    private readonly DeathScreenUI deathScreenUI;
    private GameObject currentPlayer;

    public PlayerSpawner(GridFactory gridFactory, GameObject playerPrefab, LevelObjectSpawner objectSpawner, UIRingController ringUI, PlayerHealthUIBinder healthUIBinder, DeathScreenUI deathScreenUI)
    {
        this.gridFactory = gridFactory;
        this.playerPrefab = playerPrefab;
        this.objectSpawner = objectSpawner;
        this.ringUI = ringUI;
        playerHealthUIBinder = healthUIBinder;
        this.deathScreenUI = deathScreenUI;
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
            interaction.Init(gridFactory, objectSpawner, ringUI);
        }
        else
        {
            Debug.LogWarning("Player prefab missing PlayerInteraction component!");
        }

        var health = currentPlayer.GetComponent<HealthComponent>();
        if (health != null)
        {
            playerHealthUIBinder.Bind(health);
            health.OnDied -= HandlePlayerDeath;
            health.OnDied += HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        deathScreenUI.Show("Вы погибли");
    }

}
