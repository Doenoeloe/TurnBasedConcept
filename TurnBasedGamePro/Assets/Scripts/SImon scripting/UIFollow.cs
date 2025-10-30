using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        if (player != null)
            transform.position = player.position + offset;
    }
}
