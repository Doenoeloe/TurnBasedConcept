using UnityEngine;

public class PlayerTwoHealth : Health
{
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SetHealth(100);
            Debug.Log("Player 2 health reset to 100");
        }
        /* else if (Input.GetKeyUp(KeyCode.K))
         {
             Damage(40);
             Debug.Log("Player 2 took 40 damage");
         }*/
        else if (Input.GetKeyUp(KeyCode.L))
        {
            Heal(15);
            Debug.Log("Player 2 healed 15");
        }
    }
}
