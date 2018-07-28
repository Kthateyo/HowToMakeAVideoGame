using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;

    public GameManager3 gameManager3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }

    void Pause()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        gameManager3.LoadGameMenu();
    }

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}