using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public int attackDamage = 10;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode interactKey = KeyCode.E;

    [Header("References")]
    public Transform cameraTransform; // сюда установи MainCamera или CameraHolder

    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            TryAttack();
        }

        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void TryAttack()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                Debug.Log($"Attacked {hit.collider.name} for {attackDamage} damage.");
            }
        }
    }

    void TryInteract()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance))
        {
            // Заглушка на мирное взаимодействие
            Debug.Log($"Interacted with {hit.collider.name} (not implemented).");
        }
    }
}
