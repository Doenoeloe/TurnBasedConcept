using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * move * speed * Time.deltaTime);

        // Flip root via localScale.x
        if (move > 0)
            transform.localScale = new Vector3(-1, 1, 1);  // Kijkt naar rechts
        else if (move < 0)
            transform.localScale = new Vector3(1, 1, 1); // Kijkt naar links
    }
}
