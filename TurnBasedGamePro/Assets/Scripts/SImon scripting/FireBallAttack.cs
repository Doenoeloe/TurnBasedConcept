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
        inputActions.FindActionMap("Player").Enable(); // Zorgt dat het "Player" inputmap actief is
    }

    private void Start()
    {
        fireBall = InputSystem.actions.FindAction("Attack"); // Haal de "Attack" actie op
    }

    void Update()
    {
        Aim(); // Continu bijwerken van de richtingshoek

        // Houdt bij of speler richt of schiet
        if (fireBall.IsPressed())
            isAiming = true;

        if (fireBall.WasReleasedThisFrame())
        {
            isAiming = false;
            Shoot(); // Schiet de fireball af
        }

        DrawAimLine(isAiming); // Toon richtlijn alleen tijdens het richten
    }

    // Bereken richting naar muis en draai speler daarnaar
    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Maak Fireball
    void Shoot()
    {
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
            aimLine.SetPosition(1, firePoint.position + (Vector3)aimDirection * 10f); // Lengte van de lijn
        }
        else
        {
            aimLine.enabled = false;
        }
    }
}
