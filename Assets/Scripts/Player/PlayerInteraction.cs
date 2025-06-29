using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public int attackDamage = 10;
    public float attackCooldown = 1.0f;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode interactKey = KeyCode.E;

    [Header("References")]
    public Transform cameraTransform;


    private TowerBuilder towerBuilder;
    private PlayerInventory inventory;
    private UIRingController ringUI;


    private float attackCooldownTimer = 0f;
    private bool isBuilding = false;

    public PlayerInventory GetInventory() => inventory;

    public void Init(GridFactory gridFactory, LevelObjectSpawner spawner, UIRingController ringUI)
    {
        towerBuilder = new TowerBuilder(gridFactory, spawner);
        inventory = new PlayerInventory();
        this.ringUI = ringUI;
    }

    private void Update()
    {
        HandleAttackCooldown();

        if (Input.GetKeyDown(attackKey))
        {
            TryAttack();
        }

        TryInteract(Input.GetKey(interactKey));

        if (Input.GetKeyDown(interactKey))
        {
            TryPickup();
        }
    }

    private void HandleAttackCooldown()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
            ringUI.SetMode(UIRingController.RingMode.Clockwise);
            ringUI.Show();
            ringUI.SetProgress(attackCooldownTimer / attackCooldown);

            if (attackCooldownTimer <= 0f)
            {
                ringUI.Hide();
                attackCooldownTimer = 0f;
            }
        }
    }

    void TryAttack()
    {
        if (attackCooldownTimer > 0f) return;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                Debug.Log($"Attacked {hit.collider.name} for {attackDamage} damage.");

                attackCooldownTimer = attackCooldown;

                ringUI.SetMode(UIRingController.RingMode.CounterClockwise);
                ringUI.SetProgress(0f);
                ringUI.Show();
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
                ringUI.SetMode(UIRingController.RingMode.Clockwise);
                ringUI.SetProgress(progress);
                ringUI.Show();
                isBuilding = true;
            }
            else if (isBuilding)
            {
                ringUI.Hide();
                isBuilding = false;
            }
        }
        else
        {
            if (isBuilding)
            {
                ringUI.Hide();
                isBuilding = false;
            }

            towerBuilder.UpdateBuild(new RaycastHit(), false); 
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
