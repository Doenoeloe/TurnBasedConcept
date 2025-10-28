using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField] private float force = 5f;
    [SerializeField] private ForceMode2D forceMode = ForceMode2D.Impulse;

    private void OnParticleCollision(GameObject other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb)
        {
            Vector2 dir = (rb.position - (Vector2)transform.position).normalized;
            rb.AddForce(dir * force, forceMode);
        }
    }
}
