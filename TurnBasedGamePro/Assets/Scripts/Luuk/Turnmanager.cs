using System.Collections.Generic;
using UnityEngine;

public class Turnmanager : MonoBehaviour
{
    [System.Serializable]
    sealed class PlayerData
    {
        [SerializeField] private string playerName;
        [SerializeField] private GameObject playerObject;

        internal string Name => playerName;
        internal GameObject Object => playerObject;
    }

    [Header("Players")]
    [SerializeField] private List<PlayerData> players = new List<PlayerData>();

    int currentPlayerIndex = 0;
    bool isTurnActive = false;

    void Start()
    {
        if (players.Count == 0)
        {
            Debug.LogError("No players assigned to TurnManager!");
            return;
        }

        // Subscribe to each player’s energy event
        foreach (var p in players)
        {
            var energy = p.Object.GetComponent<Energymanager>();
            var movement = p.Object.GetComponent<SimpleMovement>();
            movement.enabled = false;
            if (energy != null)
                energy.OnEnergyDepleted += HandleEnergyDepleted;
            else
                Debug.LogWarning($"{p.Name} is missing an EnergyManager!");
        }

        StartTurn();
    }

    void StartTurn()
    {
        var current = players[currentPlayerIndex];
        isTurnActive = true;
        var movement = current.Object.GetComponent<SimpleMovement>();
        movement.enabled = true;
        var energy = current.Object.GetComponent<Energymanager>();
        energy?.ResetEnergy();

        Debug.Log($"Turn started: {current.Name}");
    }

    void EndTurn()
    {
        if (!isTurnActive) return;

        isTurnActive = false;
        var current = players[currentPlayerIndex];
        var movement = current.Object.GetComponent<SimpleMovement>();
        movement.enabled = false;

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;

        Invoke(nameof(StartTurn), 0.5f);
    }

    void HandleEnergyDepleted(GameObject player)
    {
        if (players[currentPlayerIndex].Object == player)
        {
            Debug.Log($"{players[currentPlayerIndex].Name} out of energy — switching turns.");
            EndTurn();
        }
    }
}
