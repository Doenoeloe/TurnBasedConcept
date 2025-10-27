using UnityEngine;
using UnityEngine.InputSystem;

public class FireBallAttack : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float launchForce = 10f;
    [SerializeField] LineRenderer aimLine;

    [SerializeField] InputActionAsset inputActions;
    private InputAction fireBall;

    private Vector2 aimDirection;
    private bool isAiming;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void Start()
    {
        fireBall = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        Aim();

        if (fireBall.IsPressed())
            isAiming = true;

        if (fireBall.WasReleasedThisFrame())
        {
            isAiming = false;
            Shoot();
        }

        DrawAimLine(isAiming);
    }

    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(aimDirection * launchForce, ForceMode2D.Impulse);
    }

    void DrawAimLine(bool show)
    {
        if (aimLine == null) return;

        if (show)
        {
            aimLine.enabled = true;
            aimLine.SetPosition(0, firePoint.position);
            aimLine.SetPosition(1, firePoint.position + (Vector3)aimDirection * 10f); // lengte aanpassen
        }
        else
        {
            aimLine.enabled = false;
        }
    }
}
