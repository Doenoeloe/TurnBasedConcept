using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportHandler : MonoBehaviour
{
    [Header("Input & Prefabs")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject teleportMarkerPrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxTeleportDistance = 10f;
    [SerializeField] private float teleportEnergyCost = 20f;

    private InputAction teleportAction;
    private InputAction confirmAction;
    private InputAction pointAction;

    private GameObject teleportMarker;
    private bool isAiming = false;

    private Camera mainCam;
    private Energymanager energyManager;
    private Turnmanager turnManager;
    private Transform playerTransform;
    private Vector2 mousePosition;

    [SerializeField] private float markerHeightOffset = 0.5f;

    private SpellBarUI spellBar;

    private void Awake()
    {
        mainCam = Camera.main;
        playerTransform = transform;

        // Make sure each player has its own action instance
        var clonedActions = Instantiate(inputActions);
        teleportAction = clonedActions.FindAction("Attack");
        confirmAction = clonedActions.FindAction("Confirm");
        pointAction = clonedActions.FindAction("Point");

        teleportAction.performed += OnTeleportPressed;
        confirmAction.performed += OnConfirmPressed;
    }

    private void Start()
    {
        energyManager = GetComponent<Energymanager>();
        turnManager = GetComponent<Turnmanager>();
        spellBar = FindFirstObjectByType<SpellBarUI>();
    }

    private void OnEnable()
    {
        teleportAction.Enable();
        confirmAction.Enable();
        pointAction.Enable();
    }

    private void OnDisable()
    {
        teleportAction.Disable();
        confirmAction.Disable();
        pointAction.Disable();
    }

    private void OnTeleportPressed(InputAction.CallbackContext ctx)
    {
        // Only current player may act
        if (!turnManager.IsCurrentPlayer(transform.root.gameObject))
            return;

        if (spellBar.ReadCurrentSpell() != 4)
            return;

        // Not enough energy to start aiming
        if (energyManager.currentEnergy < teleportEnergyCost)
        {
            Debug.Log($"{gameObject.name}: Not enough energy to teleport!");
            return;
        }

        // Toggle aim mode
        if (isAiming)
        {
            CancelTeleport();
            return;
        }

        isAiming = true;
        teleportMarker = Instantiate(teleportMarkerPrefab);
    }

    private void OnConfirmPressed(InputAction.CallbackContext ctx)
    {
        if (!isAiming || teleportMarker == null) return;
        if (!turnManager.IsCurrentPlayer(transform.root.gameObject)) return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());
        mouseWorld.z = 0;
        
        // Clamp to max range
        if (Vector2.Distance(playerTransform.position, mouseWorld) > maxTeleportDistance)
        {
            Debug.Log($"{gameObject.name}: Target too far!");
            CancelTeleport();
            return;
        }

        // Find ground below mouse
        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.down, 100f, groundLayer);
        if (hit.collider != null)
        {
            mouseWorld = hit.point + Vector2.up * 0.5f; // safe placement above ground
        }
        else
        {
            // Optional: allow teleport anyway
            mouseWorld += Vector3.up * 0.5f;
        }

        // Try to use energy
        if (energyManager.currentEnergy >= teleportEnergyCost)
        {
            energyManager.UseEnergy(teleportEnergyCost);
            playerTransform.position = mouseWorld + Vector3.up * markerHeightOffset;

            Debug.Log($"{gameObject.name} teleported successfully!");
        }
        else
        {
            Debug.Log($"{gameObject.name}: Not enough energy!");
        }

        CancelTeleport();
    }


    private void CancelTeleport()
    {
        isAiming = false;
        if (teleportMarker != null)
            Destroy(teleportMarker);
    }

    private void Update()
    {
        if (!isAiming || teleportMarker == null)
            return;
        float distance = Vector2.Distance(playerTransform.position, teleportMarker.transform.position);
        bool inRange = distance <= maxTeleportDistance;

        teleportMarker.GetComponent<SpriteRenderer>().color = inRange ? Color.green : Color.red;
        
        Vector2 mouseWorld = mainCam.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());

        teleportMarker.transform.position = mouseWorld;
        
    }
}
