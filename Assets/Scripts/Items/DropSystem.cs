using UnityEngine;

public class DropSystem
{
    private readonly DropTable table;

    public DropSystem(DropTable table)
    {
        this.table = table;
    }

    public void Drop(Vector3 position, float force = 2f)
    {
        if (table == null || table.drops == null) return;

        foreach (var entry in table.drops)
        {
            if (entry.prefab == null) continue;

            float roll = Random.value;
            if (roll > entry.dropChance) continue;

            int amount = Random.Range(entry.quantityRange.x, entry.quantityRange.y + 1);
            for (int i = 0; i < amount; i++)
            {
                GameObject drop = Object.Instantiate(entry.prefab, position, Random.rotation);
                Rigidbody rb = drop.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 randomDir = Random.insideUnitSphere + Vector3.up;
                    rb.AddForce(randomDir.normalized * force, ForceMode.Impulse);
                }
            }
        }
    }
}
