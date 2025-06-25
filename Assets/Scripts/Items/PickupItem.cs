using UnityEngine;

public class PickupItem : MonoBehaviour, IPickup
{
    public string itemName = "Item";

    public void OnPickup(GameObject picker)
    {
        Debug.Log($"{picker.name} picked up {itemName}");

        // “ут можно добавить логику Ч например, инвентарь, увеличение счЄтчика и т.п.
        Destroy(gameObject);
    }
}
