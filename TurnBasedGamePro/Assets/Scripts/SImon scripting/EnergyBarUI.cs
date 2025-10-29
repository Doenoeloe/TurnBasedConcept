using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    [SerializeField] private Energymanager energyManager; // Link naar speler zijn energy manager
    [SerializeField] private Image energyFill; // De voorgrond van de balk

    private void Update()
    {
        if (energyManager == null || energyFill == null) return;

        // Bereken hoeveel procent energie er nog is
        float fillAmount = energyManager.currentEnergy / energyManager.maxEnergy;
        energyFill.fillAmount = fillAmount;
    }
}
