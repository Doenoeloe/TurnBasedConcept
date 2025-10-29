using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //test script
    public Transform target;       // The player’s transform
    public Vector3 offset;         // Optional offset from the player
    public float smoothSpeed = 0.125f; // Smooth follow speed

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
