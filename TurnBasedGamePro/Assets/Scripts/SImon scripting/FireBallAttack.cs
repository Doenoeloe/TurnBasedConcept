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

    private Energymanager energyManager;
    private Turnmanager turnManager;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable(); // Zorgt dat het "Player" inputmap actief is
    }

    private void Start()
    {
        fireBall = InputSystem.actions.FindAction("Attack"); // Haal de "Attack" actie op

        energyManager = GetComponentInParent<Energymanager>();
        turnManager = GetComponentInParent<Turnmanager>();
    }

    void Update()
    {
        // Alleen verder als deze speler aan de beurt is
        if (!turnManager.IsCurrentPlayer(transform.root.gameObject))
            return;

        // Alleen verder als er genoeg energie is
        if (energyManager.currentEnergy < 25)
        {
            isAiming = false;         // Zorg dat de lijn verdwijnt
            DrawAimLine(false);
            return;
        }

        // Alles hieronder gebeurt alleen als speler aan de beurt is en genoeg energie heeft
        Aim(); // Update richtingshoek

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
