using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpellBarUI : MonoBehaviour
{
    [SerializeField] Image[] spellHighlights; // drag highlight images here
    private int selectedSpellIndex;

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

