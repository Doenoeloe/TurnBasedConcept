using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : Health
{
    [SerializeField] private int healAmount = 20;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private AudioClip healSound;

    //private int currentHealth;
    private InputAction healAction;
    private Turnmanager turnManager;
    private Energymanager energyManager;
    private AudioSource audioSource;

    private void Awake()
    {
        //currentHealth = maxHealth;
        turnManager = GetComponent<Turnmanager>();
        energyManager = GetComponent<Energymanager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        healAction = inputActions.FindActionMap("Player").FindAction("Heal");
    }

    protected virtual void Update()
    {
        // Stop als het NIET jouw beurt is
        if (!turnManager.IsCurrentPlayer(gameObject))
            return;

        // Alleen healen als de speler op Heal drukt
        if (healAction.triggered)
        {
            HealPlayer();
        }
    }

    private void HealPlayer()
    {
        if (health < maxHealth && energyManager.currentEnergy >= 30)
        {
            Heal(healAmount);
            energyManager.UseEnergy(30);
            audioSource.PlayOneShot(healSound);
        }
    }
}
