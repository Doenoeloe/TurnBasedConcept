using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]int health;
    int maxHealth;
    [SerializeField] Text healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            getHealth(100);
            Debug.Log("Health reset to 100");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            Damage(60);
            Debug.Log("Took 60 damage");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Healing(health);
            Debug.Log("Healed 25 health");
        }
    }
    public int getHealth(int pHealth)
    {
        health = Mathf.Clamp(pHealth,0,maxHealth);
        settext();
        return health;
    }
    public int MaxHealth(int pMaxHealth)
    {
        maxHealth = pMaxHealth;
        return maxHealth;
    }   
    public void Damage(int damage)
    {
        health -= damage;
        settext();
    }   
    public void Healing(int pCurrentHealth)
    {
        pCurrentHealth = pCurrentHealth + 25;
        health = Mathf.Clamp(pCurrentHealth, 0, maxHealth);
        settext();
    }
    void settext()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
