using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneHealth : Health
{
    //[SerializeField] private int maxHealth = 100;
    [SerializeField] private int healAmount = 20;
    [SerializeField] private InputActionAsset inputActions;

    private int currentHealth;
    private InputAction healAction;
    private Turnmanager turnManager;
    private Energymanager energyManager;

    private AudioSource audioSource;
    [SerializeField] AudioClip healSound;

    private void Awake()
    {
        currentHealth = maxHealth;
        turnManager = GetComponent<Turnmanager>();
        energyManager = GetComponent<Energymanager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        healAction = inputActions.FindActionMap("Player").FindAction("Heal");
    }

    protected override void Update()
    {
        //Stop als het NIET jouw beurt is
        if (!turnManager.IsCurrentPlayer(gameObject))
            return;

        //Alleen healen als de speler op Heal drukt
        if (healAction.triggered)
        {
            HealPlayer();
        }
    }

    private void HealPlayer()
    {
        if (currentHealth >= maxHealth && energyManager.currentEnergy >= 30)
        {
            Heal(healAmount);
            energyManager.UseEnergy(30);
            audioSource.PlayOneShot(healSound);
        }
    }
}
