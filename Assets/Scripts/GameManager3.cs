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
    public static float SubstractFromScore = 0;

    public float SmoothPlayerReturn = 5;

    public GameObject Player;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;

    public bool ChaoticCubeRotating = false;

    bool SettingPlayerPosition = false;
    bool haveToStart = false;
    

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        startMenuUI.SetActive(true);
        startMenuUI.GetComponent<Animator>().SetTrigger("Show");

        settingsMenu.LoadSettingFromJSON();
        settingsMenu.LoadGraphicsSettingsFromJSON();
    }

    private void Update()
    {
        if (gameHasStarted)
        {
            gui.SetActive(true);
        }

        if(haveToStart && Player.GetComponent<Transform>().position.y >= 2.1f && Player.GetComponent<Transform>().position.y <= 2.4f &&
            Player.GetComponent<Transform>().position.x >= playerCollision.floor.position.x - playerCollision.floor.lossyScale.x /2 &&
            Player.GetComponent<Transform>().position.x <= playerCollision.floor.position.x + playerCollision.floor.lossyScale.x / 2)
        {
            if (Player.GetComponent<Transform>().position.y >= 2.1f && Player.GetComponent<Transform>().position.y <= 2.4f)
                haveToStart = false;

            StartGame();
        }
    }

    private void FixedUpdate()
    { 
        if (SettingPlayerPosition && !gameHasStarted)
        {
            Vector3 target = new Vector3(playerCollision.floor.position.x, 2.25f, playerCollision.floor.position.z - playerCollision.floor.lossyScale.z / 2);
            Player.transform.position = Vector3.Lerp(Player.transform.position, target, SmoothPlayerReturn * Time.deltaTime);
            
            if (Player.transform.position.y >= 2.24f && Player.transform.position.y <= 2.26f)
            {
                SettingPlayerPosition = false;
            }
        }

        if (!gameHasStarted)
        {
            float rand = Random.value;

            if (rand < 0.01f)
            {
                if (ChaoticCubeRotating)
                    playerMovement.RandomRotatePlayer(-1, 1, ForceMode.Impulse);
                else
                    playerMovement.RandomRotatePlayer(-10, 10, ForceMode.Force);
            }
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
        if(playerCollision.floor)
            SubstractFromScore = playerCollision.floor.position.z - (playerCollision.floor.lossyScale.z / 2);

        gameStartedTime = Time.time;
        gameHasStarted = true;
        gameHasEnded = false;
        gui.SetActive(true);
        ScoreUI.GetComponent<Animator>().SetTrigger("Show");
        JumpBar.SetBool("Play", true);
        FindObjectOfType<AudioManager>().Play("Wind");
        startMenuUI.GetComponent<Animator>().SetTrigger("Hide");
        Invoke("DisActivateMenu", 1f);
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<Collider>().enabled = true;
        playerMovement.forwardForce = playerMovement.ForwardStartForce;
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            Debug.Log("GAME OVER!");
            gameHasEnded = true;

            FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
            FindObjectOfType<AudioManager>().Stop("Wind");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            JumpBar.SetBool("Play", false);
            tryAgainUI.SetActive(true);
            tryAgainUI.GetComponent<Animator>().SetTrigger("Show");
            ScoreUI.GetComponent<Animator>().SetTrigger("IsEnded");
        }
    }

    public void RestartNoLoad(bool Start)
    {
        gameHasEnded = false;
        gameHasStarted = false;

        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        ScoreUI.GetComponent<Animator>().SetTrigger("Hide");
        tryAgainUI.GetComponent<Animator>().SetTrigger("Hide");
        Invoke("DisActivateTryAgainUI", 1f);

        Player.GetComponent<Rigidbody>().useGravity = false;

        SettingPlayerPosition = true;
        Player.GetComponent<Collider>().enabled = false;
        if (Start)
        {
            haveToStart = true;
        }
        else
        {
            startMenuUI.SetActive(true);
            Invoke("ShowMenu", 0.5f);
            //startMenuUI.GetComponent<Animator>().SetTrigger("Show");
        }
    }

    void DisActivateMenu()
    {
        startMenuUI.SetActive(false);
    }

    void DisActivateTryAgainUI()
    {
        tryAgainUI.SetActive(false);
    }

    void ShowMenu()
    {
        startMenuUI.GetComponent<Animator>().SetTrigger("Show");
    }
}
