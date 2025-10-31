using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Energymanager : MonoBehaviour
{
  [SerializeField] InputActionAsset inputActions;
    private InputAction inputCheck;
    [Header("Energy Settings")]
    public float maxEnergy = 50f;
    public float currentEnergy;

    [SerializeField] Turnmanager turnManager;


    internal event Action<GameObject> OnEnergyDepleted;
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void Awake()
    {
        inputCheck = InputSystem.actions.FindAction("Move");
    }
    void Start()
    {
        turnManager = GetComponent<Turnmanager>();

        ResetEnergy();
    }

    void FixedUpdate()
    {
        if (inputCheck.IsPressed())
        {
            UseEnergy(0.1f);

        }


    }

    public void UseEnergy(float amount)
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
