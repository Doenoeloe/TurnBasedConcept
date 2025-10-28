using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lightning : MonoBehaviour
{
    [SerializeField] GameObject lightningPrefab;          
    [SerializeField] GameObject lightningExplosionEffect; 

    [SerializeField] InputActionAsset inputActions;       
    private InputAction lightning;               

    private void OnEnable()
    {
        // Activeer de inputmap voor de speler zodat "Lightning" input werkt
        inputActions.FindActionMap("Player").Enable();
    }

    void Start()
    {
        // Zoek de "Lightning" actie uit het Input System
        lightning = InputSystem.actions.FindAction("Lightning");
    }

    void Update()
    {
        // Controleer of de lightning-knop is ingedrukt (triggered = éénmalig bij indrukken)
        if (lightning.triggered)
        {
            CastLightning();
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
