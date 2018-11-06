using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager3 : MonoBehaviour
{
    public SettingsMenu settingsMenu;
    
    public GameObject tryAgainUI;
    public GameObject startMenuUI;
    public GameObject gui;
    public Animator ScoreUI;
    public Animator JumpBar;

    public static bool gameHasEnded = false;
    public static bool gameHasStarted = false;
    public static float gameStartedTime;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        startMenuUI.SetActive(true);

        settingsMenu.LoadSettingFromJSON();
        settingsMenu.LoadGraphicsSettingsFromJSON();
    }

    private void Update()
    {
        if (gameHasStarted)
        {
            gui.SetActive(true);
            startMenuUI.SetActive(false);
        }
    }

    public void LoadGameMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameHasStarted = false;
        gameHasEnded = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameHasStarted = false;
        gameHasEnded = false;
        StartGame();
    }

    public void StartGame()
    {
        gameStartedTime = Time.time;
        gameHasStarted = true;
        gameHasEnded = false;
        gui.SetActive(true);
        JumpBar.SetBool("Play", true);
        FindObjectOfType<AudioManager>().Play("Wind");
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
            FindObjectOfType<AudioManager>().Stop("Wind");

            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            ScoreUI.GetComponent<Animator>().SetTrigger("IsEnded");
            JumpBar.SetBool("Play", false);
            gameHasEnded = true;

            Debug.Log("GAME OVER!");
            tryAgainUI.SetActive(true);
        }
    }
}
