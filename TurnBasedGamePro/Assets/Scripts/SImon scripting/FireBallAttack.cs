using UnityEngine;
using UnityEngine.InputSystem;

public class FireBallAttack : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private LineRenderer aimLine;

    [Header("Input Settings")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction input;

    private Vector2 aimDirection;
    private bool isAiming;

    private Energymanager energyManager;
    private Turnmanager turnManager;
    private Animator animator;

    private SpellBarUI spellBar;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void Start()
    {
        input = InputSystem.actions.FindAction("Attack");

        energyManager = GetComponentInParent<Energymanager>();
        turnManager = GetComponentInParent<Turnmanager>();

        animator = GetComponentInParent<Animator>();

        spellBar = FindFirstObjectByType<SpellBarUI>();
    }

    void Update()
    {
        if (!turnManager.IsCurrentPlayer(transform.root.gameObject))
            return;

        if (spellBar.ReadCurrentSpell() != 1)
            return;

        Aim();

        if (energyManager.currentEnergy < 25)
        {
            isAiming = false;
            DrawAimLine(false);
            return;
        }

        if (input.IsPressed())
            isAiming = true;

        if (input.WasReleasedThisFrame())
        {
            isAiming = false;
            Shoot();
            energyManager.UseEnergy(25f);
        }

        DrawAimLine(isAiming);
    }


    // Bereken richting naar muis en draai speler daarnaar
    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        bool isFlipped = transform.root.localScale.x < 0;

        if (isFlipped)
            transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        else
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Maak Fireball en speel animatie af
    void Shoot()
    {
        // Speel animatie
        if (animator != null)
            animator.SetTrigger("FireBall");

        // Instantieer de fireball
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(aimDirection * launchForce, ForceMode2D.Impulse);
    }

    // Tekent richtlijn (alleen zichtbaar tijdens richten)
    void DrawAimLine(bool show)
    {
        if (aimLine == null) return;

        if (show)
        {
            aimLine.enabled = true;
            aimLine.SetPosition(0, firePoint.position);
            aimLine.SetPosition(1, firePoint.position + (Vector3)aimDirection * 10f);
        }
        else
        {
            aimLine.enabled = false;
        }
    }
}
