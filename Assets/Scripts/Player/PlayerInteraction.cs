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

    [SerializeField] private UIRingController buildProgressUI;


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

        if (Input.GetKeyDown(interactKey))
        {
            TryPickup();
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

    void TryInteract(bool isHolding)
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance))
        {
            float progress = towerBuilder.UpdateBuild(hit, isHolding);

            if (progress > 0f)
            {
                buildProgressUI.Show();
                buildProgressUI.SetProgress(progress);
            }
            else
            {
                buildProgressUI.Hide();
            }
        }
        else
        {
            towerBuilder.UpdateBuild(new RaycastHit(), false);
            buildProgressUI.Hide();
        }
    }



    void TryPickup()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance))
        {
            var pickup = hit.collider.GetComponent<IPickup>();
            if (pickup != null)
            {
                pickup.OnPickup(gameObject);
            }
        }
    }



}

