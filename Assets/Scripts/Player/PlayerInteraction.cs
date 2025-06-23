using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public int attackDamage = 10;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode interactKey = KeyCode.E;

    [Header("References")]
    public Transform cameraTransform;


    private TowerBuilder towerBuilder;

    public void Init(GridFactory gridFactory, LevelObjectSpawner spawner)
    {
        towerBuilder = new TowerBuilder(gridFactory, spawner);
    }

    private void Update()
    {



        if (Input.GetKeyDown(attackKey))
        {
            TryAttack();
        }

        // Постройка при удержании
        TryInteract(Input.GetKey(interactKey));
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

    void TryInteract(bool isHolding)
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance))
        {
            bool built = towerBuilder.UpdateBuild(hit, isHolding);

            if (built)
                Debug.Log("Tower successfully built!");
        }
        else
        {
            // Сброс, если ничего не под прицелом
            towerBuilder.UpdateBuild(new RaycastHit(), false);
        }
    }



}

