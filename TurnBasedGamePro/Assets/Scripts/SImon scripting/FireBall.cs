using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] SpellsData spellData;

    void Start()
    {
        Destroy(gameObject, spellData.LifeTime);
    }

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != spellData.PlayerLayer)
        {
            Destroy(gameObject);

            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
