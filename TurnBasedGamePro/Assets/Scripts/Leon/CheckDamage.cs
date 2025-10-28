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

    private bool hasExploded = false; // Tracks if the explosion already happened

    public void DamageFireball()
    {
        print("FireBall");
        if (hasExploded) return; // Only do this once
        hasExploded = true;

        // Get the maximum radius (outermost ring)
        float maxRadius = 0f;
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
            foreach (var ring in damageRings)
            {
                if (distance <= ring.radius)
                {
                    //Debug.Log($"{hit.name} takes {ring.damage} damage! (Distance: {distance:F2})");

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
