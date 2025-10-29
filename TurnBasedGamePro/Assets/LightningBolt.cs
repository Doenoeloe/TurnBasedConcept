using UnityEngine;

public class LightningBolt : CheckDamage
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] SpellsData spellData;

    [SerializeField] AudioClip boltSound;
    [SerializeField] AudioClip explosionSound;

    private AudioSource audioSource;

    private bool isEnemyHit = false;

    // Koppel de audioSource
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        // Vernietig na de ingestelde levensduur en speel de boltSound af
        audioSource.PlayOneShot(boltSound);
        Destroy(gameObject, spellData.LifeTime);
    }

    void OnParticleCollision(GameObject other)
    {
        if (isEnemyHit) return;

        //if (other.layer == spellData.EnemyLayer)
        //{
            // Damage
            DamageSpell();
            Debug.Log("DamageLightning");

            GameObject temp = new GameObject("TempAudio");
            temp.transform.position = transform.position;
            AudioSource a = temp.AddComponent<AudioSource>();
            a.clip = explosionSound;
            a.spatialBlend = 0f; // 2D geluid
            a.Play();
            Destroy(temp, explosionSound.length);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            isEnemyHit = true;
        //}
    }

}
