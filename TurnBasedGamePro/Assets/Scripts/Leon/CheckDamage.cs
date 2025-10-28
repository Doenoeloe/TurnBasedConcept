using UnityEngine;

[System.Serializable]
public class DamageRing
{
    public float radius;   // Radius of this ring
    public int damage;     // Damage for objects inside this ring
}

public class CheckDamage : MonoBehaviour
{
    [SerializeField] private LayerMask damageLayers;
    [SerializeField] private DamageRing[] damageRings;  // Array of rings, inner first
    float maxRadius;

    private void Start()
    {
       maxRadius = 0f;
    }
    private void Update()
    {
        checkDamage();
    }
    void checkDamage()
    {

        // Get the maximum radius (outermost ring)

        foreach (var ring in damageRings)
        {
            if (ring.radius > maxRadius) maxRadius = ring.radius;
        }

        // Get all colliders in the max radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, maxRadius, damageLayers);
        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            // Find which ring this object falls into
            bool hitRegistered = false;
            foreach (var ring in damageRings)
            {
                if (distance <= ring.radius)
                {
                    Debug.Log($"{hit.name} takes {ring.damage} damage! (Distance: {distance:F2})");
                    hitRegistered = true;

                    // Example: apply damage
                    // hit.GetComponent<Health>()?.TakeDamage(ring.damage);
                    break; // Stop checking further rings once a match is found
                }
            }

            
        }
    }
   
    void OnDrawGizmosSelected()
    {
        if (damageRings == null) return;

        foreach (var ring in damageRings)
        {
            Gizmos.color = Color.Lerp(Color.red, Color.yellow, ring.radius / 10f);
            Gizmos.DrawWireSphere(transform.position, ring.radius);
        }
    }
}
