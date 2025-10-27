using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getHealth(int pHealth)
    {
        health = pHealth;
        return health;
    }
    public void Damage(int damage)
    {
        health -= damage;
    }   
    public void Healing(int pCurrentHealth)
    {
        pCurrentHealth =+ health/100*20;
    }
}
