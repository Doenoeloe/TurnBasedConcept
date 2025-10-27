using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Spells Data")]
public class SpellsData : ScriptableObject
{
    [SerializeField] private string spellName;
    [SerializeField] private float damage;
    [SerializeField] int playerLayer;
    [SerializeField] float lifetime;

    // Public properties (read-only or read/write)
    public string SpellName
    {
        get => spellName;
        set => spellName = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Max(0, value);
    }
    
    public int PlayerLayer
    {
        get => playerLayer;
        set => playerLayer = value;
    }
    
    public float LifeTime
    {
        get => lifetime;
        set => lifetime = value;
    }
}
