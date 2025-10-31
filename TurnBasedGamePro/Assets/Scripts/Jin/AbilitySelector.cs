using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellBarUI : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    // adds multiple objects into the array
    [SerializeField] Image[] spellHighlights;
    
    int selectedSpellIndex;

    // creates an empty input action for each actions.
    InputAction fireBall;
    InputAction lightning;
    InputAction healing;
    InputAction teleport;

    /// <summary>
    /// This function only gets called when the object becomes active.
    /// </summary>
    private void OnEnable()
    {
        // enables the actionmap, for use to initialize the empty fields.
        inputActions.FindActionMap("Player").Enable();
    }
    void Awake()
    {
        // fills the empty fields with actions from the Player actionmap
        fireBall = InputSystem.actions.FindAction("Fireball");
        lightning = InputSystem.actions.FindAction("Lightning");
        healing = InputSystem.actions.FindAction("Heal");
        teleport = InputSystem.actions.FindAction("Teleport");
    }

    void Update()
    {
        // if statements to check which action is being pressed
        if (fireBall.IsPressed())
            SelectSpell(0);
        else if (lightning.IsPressed())
            SelectSpell(1);
        else if (healing.IsPressed())
            SelectSpell(2);
        else if (teleport.IsPressed())
            SelectSpell(3);
    }

    /// <summary>
    /// This function will take in the given index (based on action) and it will highlight that spell only, turning off all the other spells before doing so.
    /// </summary>
    /// <param name="index"></param>
    void SelectSpell(int index)
    {
        for (int i = 0; i < spellHighlights.Length; i++)
        {
            // make sure the highlight exists before toggling
            if (spellHighlights[i] != null)
                spellHighlights[i].gameObject.SetActive(i == index);
        }

        selectedSpellIndex = index + 1;
    }

    /// <summary>
    /// This function will check if the player is currently selected a spell.
    /// </summary>
    /// <returns>Returns the index of the selected spell</returns>
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

