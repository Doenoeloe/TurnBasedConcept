using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] private int healAmount = 20;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private AudioClip healSound;

    //private int currentHealth;
    private InputAction input;
    private Turnmanager turnManager;
    private Energymanager energyManager;
    private AudioSource audioSource;

    private SpellBarUI spellBar;

    [SerializeField] GameObject winUI;
    [SerializeField] float mainMenuDelay = 3f;
    private float timer;

    private void Awake()
    {
        //currentHealth = maxHealth;
        turnManager = GetComponent<Turnmanager>();
        energyManager = GetComponent<Energymanager>();
        audioSource = GetComponent<AudioSource>();
        spellBar = FindFirstObjectByType<SpellBarUI>();
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        input = inputActions.FindActionMap("Player").FindAction("Attack");
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            winUI.SetActive(true);
            timer += Time.deltaTime;
            if (timer > mainMenuDelay)
            {
                SceneManager.LoadScene("Jin_MainMenu_Scene");
            }
        }
        // Stop als het NIET jouw beurt is
        if (!turnManager.IsCurrentPlayer(gameObject))
            return;

        if (spellBar.ReadCurrentSpell() != 3)
            return;

        // Alleen healen als de speler op Heal drukt
        if (input.triggered)
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
