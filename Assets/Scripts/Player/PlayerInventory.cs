using System.Collections.Generic;

public class PlayerInventory
{
    private readonly Dictionary<string, int> resources = new();

    public void Add(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
            resources[resourceType] += amount;
        else
            resources[resourceType] = amount;

        UIManager.Instance?.UpdateResource(resourceType, resources[resourceType]);
    }

    public int GetAmount(string resourceType)
    {
        return resources.TryGetValue(resourceType, out var amount) ? amount : 0;
    }

    public Dictionary<string, int> GetAllResources()
    {
        return new Dictionary<string, int>(resources); // копия, чтобы избежать внешнего изменения
    }
}
