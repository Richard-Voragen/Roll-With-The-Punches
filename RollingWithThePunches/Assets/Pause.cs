using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject blackScreen;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; //Toggle the pause state
        blackScreen.SetActive(isPaused); //Show/hide UI element

        if (isPaused)
        {
            Time.timeScale = 0f; //Pause
        }
        else
        {
            Time.timeScale = 1f; //Unpause
        }
    }
}
