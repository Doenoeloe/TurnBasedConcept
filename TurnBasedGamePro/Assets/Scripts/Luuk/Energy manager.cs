using System;
using UnityEngine;

public class Energymanager : MonoBehaviour
{
    [Header("Energy Settings")]
    [SerializeField] private float maxEnergy = 50f;
    [SerializeField] private float currentEnergy;

    
    internal event Action<GameObject> OnEnergyDepleted;

    void Start()
    {
        ResetEnergy();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
            UseEnergy(0.1f);

        if (Input.GetKey(KeyCode.A))
            UseEnergy(0.1f);

        if (Input.GetKeyDown(KeyCode.F))
            UseEnergy(25f);
    }

    void UseEnergy(float amount)
    {
        if (amount <= 0f) return;

        currentEnergy -= amount;

        if (currentEnergy <= 0f)
        {
            currentEnergy = 0f;
            HandleEnergyDepleted();
        }
    }

    void HandleEnergyDepleted()
    {
        Debug.Log($"{gameObject.name} has run out of energy!");
        OnEnergyDepleted?.Invoke(gameObject);
    }

    internal void ResetEnergy()
    {
        currentEnergy = maxEnergy;
    }

    internal float GetEnergy() => currentEnergy;
}
