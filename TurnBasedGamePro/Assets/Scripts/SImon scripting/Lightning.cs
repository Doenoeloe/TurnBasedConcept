using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lightning : MonoBehaviour
{
    [SerializeField] GameObject lightningPrefab;
    [SerializeField] GameObject lightningExplosionEffect;

    [SerializeField] InputActionAsset inputActions;
    private InputAction lightning;

    [SerializeField] SpellsData spellData;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    void Start()
    {
        lightning = InputSystem.actions.FindAction("Lightning");
    }


    void Update()
    {
        if (lightning.triggered)
        {
            CastLightning();
        }
    }

    private void CastLightning()
    {
        print("lightnin");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        GameObject lightning = Instantiate(lightningPrefab, mousePos, lightningPrefab.transform.rotation);

        StartCoroutine(DestroyAfterDelay(lightning, spellData.LifeTime));
    }

    private IEnumerator DestroyAfterDelay(GameObject bolt, float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(lightningExplosionEffect, bolt.transform.position, Quaternion.identity);

        Destroy(bolt);
    }

}
