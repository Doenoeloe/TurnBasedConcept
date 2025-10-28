using UnityEngine;

public class CheckDamage : MonoBehaviour
{
    [SerializeField] LayerMask damageLayers;
    [SerializeField] float checkRadius;

    public void Check(Vector2 pPosition)
    {
        Collider[] hits = Physics.OverlapSphere(pPosition, checkRadius);

        foreach (Collider hit in hits)
        {
            // Check if hit's layer is in damageLayers
            if ((damageLayers.value & (1 << hit.gameObject.layer)) != 0)
            {
                Debug.Log($"{hit.name} is on a damage layer: {LayerMask.LayerToName(hit.gameObject.layer)}");
            }
        }
    }
}
