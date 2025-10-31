using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth = 100;

    protected virtual void Start()
    {
        health = maxHealth;
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
