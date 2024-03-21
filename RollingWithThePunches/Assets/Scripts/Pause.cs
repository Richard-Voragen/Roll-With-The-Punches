using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject blackScreen; // Reference to the UI element
    private bool isPaused = false; // Tracks the pause state of the game

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // Toggle the pause state
        blackScreen.SetActive(isPaused); // Show/hide the UI element

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
        }
    }
}
