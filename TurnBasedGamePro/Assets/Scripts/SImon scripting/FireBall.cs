using UnityEngine;
using UnityEngine.Rendering;

public class FireBall : CheckDamage
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] SpellsData spellData;

    [SerializeField] AudioClip whooshSound;
    [SerializeField] AudioClip explosionSound;

    private AudioSource audioSource;

    // Koppel de audioSource
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Verwijder fireball automatisch na een bepaalde tijd en play de whoosSound
        audioSource.PlayOneShot(whooshSound);
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
                DamageFireball();
                print("DamageFire");
            }
            // Spawn visueel effect
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            DamageFireball();
            // Speel explosie geluid op dezelfde plek, onafhankelijk van dit object
            GameObject temp = new GameObject("TempAudio");
            temp.transform.position = transform.position;
            AudioSource a = temp.AddComponent<AudioSource>();
            a.clip = explosionSound;
            a.spatialBlend = 0f; // 2D geluid
            a.Play();
            Destroy(temp, explosionSound.length);

            // Vernietig de fireball
            Destroy(gameObject);
        }
    }
}
