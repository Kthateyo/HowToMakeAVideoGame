using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager3 : MonoBehaviour
{
    public float restartDelay;
    public GameObject tryAgainUI;
    public GameObject startMenuUI;
    public GameObject scoreUI;

    public GameObject player;
    public GameObject levelManager;

    public static bool gameHasEnded = false;

    public static bool gameHasStarted = false;
    public static float gameStartedTime;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        startMenuUI.SetActive(true);
    }

    private void Update()
    {
        if (gameHasStarted)
        {
            scoreUI.SetActive(true);
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

        FindObjectOfType<AudioManager>().Play("Wind");
        Invoke("DisableMenuStart", 2);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
            FindObjectOfType<AudioManager>().Stop("Wind");

            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            scoreUI.GetComponent<Animator>().SetTrigger("IsEnded");
            gameHasEnded = true;

            Debug.Log("GAME OVER!");
            tryAgainUI.SetActive(true);
        }
    }
}
