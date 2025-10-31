using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] InputActionAsset inputActions;

    private InputAction escape;
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    void Awake()
    {
        escape = InputSystem.actions.FindAction("Pause");
    }
    void Update()
    {
        if (escape.WasReleasedThisFrame())
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 

        SceneManager.LoadScene("MainMenu");
    }
}
