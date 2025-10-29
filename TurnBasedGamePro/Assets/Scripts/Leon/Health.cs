using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth = 100;

    protected virtual void Start()
    {
        health = maxHealth;
    }

    protected virtual void Update()
    {
        // Base health doesn’t need to handle input directly.
        // Input will be handled in child classes.
        print(health + gameObject.name);
    }

    public virtual void SetHealth(int pHealth)
    {
        health = Mathf.Clamp(pHealth, 0, maxHealth);
    }

    public virtual void Damage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public virtual void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

    }

  
}
