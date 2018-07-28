using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelInfinityProcedural");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
