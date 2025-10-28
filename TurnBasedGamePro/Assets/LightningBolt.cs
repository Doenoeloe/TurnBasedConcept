using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] SpellsData spellData;
    private bool isEnemyHit = false;

    void Start()
    {
        // Vernietig na de ingestelde levensduur
        Destroy(gameObject, spellData.LifeTime);
    }

    void OnParticleCollision(GameObject other)
    {
        if (isEnemyHit) return;

        if (other.layer == spellData.EnemyLayer)
        {
            // Damage
            Debug.Log("DamageLightning");
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            isEnemyHit = true;
        }
    }

}
