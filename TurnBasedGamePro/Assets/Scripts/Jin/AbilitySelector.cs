using UnityEngine;
using UnityEngine.UI;

public class SpellBarUI : MonoBehaviour
{
    [SerializeField] private Image[] spellHighlights; // Assign in inspector
    private int selectedSpellIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Q))
            SelectSpell(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.W))
            SelectSpell(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.E))
            SelectSpell(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.R))
            SelectSpell(3);
    }

    void SelectSpell(int index)
    {
        // Disable all highlights first
        for (int i = 0; i < spellHighlights.Length; i++)
            spellHighlights[i].enabled = (i == index);

        selectedSpellIndex = index;
        Debug.Log("Selected Spell: " + (index + 1));
    }
}
