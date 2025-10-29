using UnityEngine;

public class PlayerOneHealth : Health
{
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            SetHealth(100);
            Debug.Log("Player 1 health reset to 100");
        }
        /* else if (Input.GetKeyUp(KeyCode.A))
         {
             Damage(40);
             Debug.Log("Player 1 took 40 damage");
         }*/
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Heal(25);
            Debug.Log("Player 1 healed 25");
        }
    }
}
