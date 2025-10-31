using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lightning : MonoBehaviour
{
    [SerializeField] GameObject lightningPrefab;          
    [SerializeField] GameObject lightningExplosionEffect; 

    [SerializeField] InputActionAsset inputActions;       
    private InputAction input;

    private Energymanager energyManager;
    private Turnmanager turnManager;

    private SpellBarUI spellBar;

    private void OnEnable()
    {
        // Activeer de inputmap voor de speler zodat "Lightning" input werkt
        inputActions.FindActionMap("Player").Enable();
    }

    void Start()
    {
        // Zoek de "Lightning" actie uit het Input System
        input = InputSystem.actions.FindAction("Attack");

        energyManager = GetComponentInParent<Energymanager>();
        turnManager = GetComponentInParent<Turnmanager>();

        spellBar = FindFirstObjectByType<SpellBarUI>();
    }

    void Update()
    {
        // Alleen verder als deze speler aan de beurt is
        if (!turnManager.IsCurrentPlayer(transform.root.gameObject))
            return;

        if (spellBar.ReadCurrentSpell() != 2)
            return;

        // Alleen verder als er genoeg energie is
        if (energyManager.currentEnergy < 25) // bijvoorbeeld 25 energy voor lightning
            return;

        // Lightning actie triggered?
        if (input.triggered)
        {
            CastLightning();
            energyManager.UseEnergy(25); // verbruik energie
        }
    }

    // Roept een bliksem-aanval op bij de muispositie
    private void CastLightning()
    {
        // Bereken wereldpositie van muis
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Spawn de bliksem met originele prefabrotatie
        GameObject lightning = Instantiate(lightningPrefab, mousePos, lightningPrefab.transform.rotation);

    }
}
