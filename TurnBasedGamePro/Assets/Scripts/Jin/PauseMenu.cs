using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Creates a game object field that is used for the PauseUI
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] InputActionAsset inputActions;

    InputAction escape;
    bool gameIsPaused = false;
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
            // If the game is paused it will resume the game, otherwise it will pause it, the bool flag will be reversed by each statement.
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }
    /// <summary>
    /// This function resumes the game, setting the timescale back to 1. and sets the pause UI to inactive.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    /// <summary>
    /// This function will pause the game, setting the UI to active, and setting the timescale to 0
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Jin_MainMenu_Scene");
    }
}
