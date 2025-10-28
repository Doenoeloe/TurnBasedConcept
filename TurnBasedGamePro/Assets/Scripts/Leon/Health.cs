using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected TMP_Text healthText;

    protected virtual void Start()
    {
        health = maxHealth;
        SetText();
    }

    protected virtual void Update()
    {
        // Base health doesn’t need to handle input directly.
        // Input will be handled in child classes.
    }

    public virtual void SetHealth(int pHealth)
    {
        health = Mathf.Clamp(pHealth, 0, maxHealth);
        SetText();
    }

    public virtual void Damage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        SetText();
    }

    public virtual void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        SetText();
    }

    protected void SetText()
    {
        if (healthText != null)
            healthText.text = $"Health: {health}";
    }
}
