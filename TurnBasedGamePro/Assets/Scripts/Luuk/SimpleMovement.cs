using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed

    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrows)
        float move = Input.GetAxis("Horizontal");

        // Move the player left/right
        transform.Translate(Vector3.right * move * speed * Time.deltaTime);
    }
}
