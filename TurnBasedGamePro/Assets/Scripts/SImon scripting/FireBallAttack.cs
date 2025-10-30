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
    private InputAction fireBall;

    private Vector2 aimDirection;
    private bool isAiming;

    private Energymanager energyManager;
    private Turnmanager turnManager;
    private Animator animator; // Animator toegevoegd

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void Start()
    {
        fireBall = InputSystem.actions.FindAction("Attack");

        energyManager = GetComponentInParent<Energymanager>();
        turnManager = GetComponentInParent<Turnmanager>();

        // Animator ophalen van de parent (speler)
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        // Alleen verder als deze speler aan de beurt is
        if (!turnManager.IsCurrentPlayer(transform.root.gameObject))
            return;

        Aim(); // Update richtingshoek

        // Alleen verder als er genoeg energie is
        if (energyManager.currentEnergy < 25)
        {
            isAiming = false;
            DrawAimLine(false);
            return;
        }

        if (fireBall.IsPressed())
            isAiming = true;

        if (fireBall.WasReleasedThisFrame())
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
