using UnityEngine;

public class PickupItem : MonoBehaviour, IPickup
{
    public string itemName = "Item";

    public void OnPickup(GameObject picker)
    {
        Debug.Log($"{picker.name} picked up {itemName}");

        // ��� ����� �������� ������ � ��������, ���������, ���������� �������� � �.�.
        Destroy(gameObject);
    }
}
