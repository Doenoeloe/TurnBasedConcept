using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;   
    [SerializeField] SpellsData spellData;         

    void Start()
    {
        // Verwijder fireball automatisch na een bepaalde tijd
        Destroy(gameObject, spellData.LifeTime);
    }

    void Update()
    {
        // Haal Rigidbody2D op om richting aan te passen aan de snelheid
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            // Draai de fireball zodat hij meedraait met zijn vlieg richting
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Wordt aangeroepen wanneer de fireball iets raakt
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Alleen reageren als het NIET de spelerlayer is
        if (collision.gameObject.layer != spellData.PlayerLayer)
        {
            if (collision.gameObject.layer == spellData.EnemyLayer)
            {
                // Damage
                print("DamageFire");
            }
            // Destroy de fireball
            Destroy(gameObject);

            // Maak explosie-effect aan op de impactpositie
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
