using UnityEngine;

public class ResourcePickup : MonoBehaviour, IPickup
{
    [SerializeField] private string resourceType = "wood";
    [SerializeField] private int amount = 1;

    public void OnPickup(GameObject picker)
    {
        var interaction = picker.GetComponent<PlayerInteraction>();
        if (interaction != null)
        {
            interaction.GetInventory().Add(resourceType, amount);
        }

        Destroy(gameObject);
    }
}

