using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthFill; // De voorgrond van de balk
    [SerializeField] PlayerHealth playerHealth; // Referentie naar de PlayerHealth component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (healthFill != null)
        {
            // Bereken hoeveel procent gezondheid er nog is
            float fillAmount = (float)playerHealth.health / (float)playerHealth.maxHealth;
            healthFill.fillAmount = fillAmount;
            print(fillAmount + gameObject.name);
        }
    }
}
