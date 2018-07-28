using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay;
    public GameObject completeLevelUI;

    bool gameHasEnded = false;

    public void LevelComplete()
    {
        completeLevelUI.SetActive(true);
        Debug.Log("LEVEL WON!");
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER!");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
