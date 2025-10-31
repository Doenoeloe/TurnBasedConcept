using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour

{
    [Header("References")]
    [SerializeField] CinemachineCamera virtualCamera;
    private Turnmanager turnManager;

    [Space(5)]

    [Header("Zoom Settings")]
    private float zoomInFOV = 20f;
    private float zoomOutFOV = 9.99f;
    private float zoomSpeed = 2f;

    [SerializeField] private GameObject currentPlayer;
    [SerializeField] private GameObject firstPlayer;

    private void Start()
    {
        currentPlayer = firstPlayer;
        turnManager = currentPlayer.GetComponent<Turnmanager>();
    }

    private void Update()
    {
        if (turnManager.GetCurrentPlayerObject() != currentPlayer)
        {
            currentPlayer = turnManager.GetCurrentPlayerObject();
            Debug.Log("hallo mijn naam is " + currentPlayer.name);
            turnManager = currentPlayer.GetComponent<Turnmanager>();
        }

      
        float currentZoom = virtualCamera.Lens.OrthographicSize;
        virtualCamera.Target.TrackingTarget = currentPlayer.transform;
        StartCoroutine(SmoothZoom(zoomInFOV));


    }

    private IEnumerator SmoothZoom(float targetFOV)
    {
        var cam = virtualCamera.Lens;
        float startFOV = cam.OrthographicSize;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            cam.OrthographicSize = Mathf.Lerp(startFOV, targetFOV, t);
            virtualCamera.Lens = cam;
            yield return null;
        }
    }

}
