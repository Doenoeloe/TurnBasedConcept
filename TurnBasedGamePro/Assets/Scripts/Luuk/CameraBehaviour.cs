using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("References")]
    private CinemachineTargetGroup targetGroup;
    private CinemachineCamera virtualCamera;
    private Turnmanager turnManager; // drag in Inspector

    [Header("Zoom Settings")]
    private float zoomInWeight = 1.0f;
    private float zoomOutWeight = 0.3f;
    private float zoomInFOV = 30f;
    private float zoomOutFOV = 50f;
    private float zoomSpeed = 2f;

    private Coroutine zoomRoutine;

    private void Start()
    {
        if (turnManager == null || targetGroup == null || virtualCamera == null)
        {
            Debug.LogError("Missing references on CinemachineZoomManager!", this);
            return;
        }

        // Subscribe to events
        turnManager.OnTurnChanged += HandleTurnChanged;

        // Optional: hook into attacks or energy depletion later
        foreach (var player in FindObjectsOfType<Energymanager>())
        {
            player.OnEnergyDepleted += HandlePlayerEnergyDepleted;
        }

        // Initial zoom
        HandleTurnChanged(0);
    }

    private void HandleTurnChanged(int newPlayerIndex)
    {
        var newPlayer = turnManager.GetCurrentPlayerObject();
        ZoomOnPlayer(newPlayer);
    }

    private void HandlePlayerEnergyDepleted(GameObject player)
    {
        // Zoom out briefly when player’s energy is gone or they attack
        ZoomOut();
    }

    private void ZoomOnPlayer(GameObject player)
    {
        if (player == null) return;

        // Set all weights low, except this player
        //for (int i = 0; i < targetGroup.Targets.Length; i++)
        //{
        //    var t = targetGroup.Targets[i];
        //    t.Weight = (t.Object == player.transform) ? zoomInWeight : 0f;
        //    targetGroup.Targets[i] = t;
        //}

        StartZoom(zoomInFOV);
    }

    private void ZoomOut()
    {
        // Even out weights slightly (show all)
        //for (int i = 0; i < targetGroup.Targets.Length; i++)
        //{
        //    var t = targetGroup.Targets[i];
        //    t.Weight = zoomOutWeight;
        //    targetGroup.Targets[i] = t;
        //}

        StartZoom(zoomOutFOV);
    }

   private void StartZoom(float targetFOV)
    {
        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);
        zoomRoutine = StartCoroutine(SmoothZoom(targetFOV));
    }

   private IEnumerator SmoothZoom(float targetFOV)
    {
        var cam = virtualCamera.Lens;
        float startFOV = cam.FieldOfView;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            cam.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            virtualCamera.Lens = cam;
            yield return null;
        }
    }
}
