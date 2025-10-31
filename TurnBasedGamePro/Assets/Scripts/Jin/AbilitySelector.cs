using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellBarUI : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Image[] spellHighlights; // drag highlight images here
    private int selectedSpellIndex;

    private InputAction fireBall;
    private InputAction lightning;
    private InputAction healing;
    private InputAction teleport;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    void Awake()
    {
        fireBall = InputSystem.actions.FindAction("Fireball");
        lightning = InputSystem.actions.FindAction("Lightning");
        healing = InputSystem.actions.FindAction("Heal");
        teleport = InputSystem.actions.FindAction("Teleport");
    }

    void Update()
    {
        if (fireBall.IsPressed())
            SelectSpell(0);
        else if (lightning.IsPressed())
            SelectSpell(1);
        else if (healing.IsPressed())
            SelectSpell(2);
        else if (teleport.IsPressed())
            SelectSpell(3);
    }

    void SelectSpell(int index)
    {
        for (int i = 0; i < spellHighlights.Length; i++)
        {
            // make sure the highlight exists before toggling
            if (spellHighlights[i] != null)
                spellHighlights[i].gameObject.SetActive(i == index);
        }

        selectedSpellIndex = index + 1;
        Debug.Log("Selected Spell: " + (index));
    }
    public int ReadCurrentSpell()
    {
        if (selectedSpellIndex != null)
        {
            return selectedSpellIndex;
        }
        else
        {
            return 0;   
        }
    }
}

