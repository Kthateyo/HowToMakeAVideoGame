using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour {

    public void StandardButton()
    {
        SceneManager.LoadScene("Level01");
    }

    public void InfinityRandomsButton()
    {
        SceneManager.LoadScene("LevelInfinityRandom");
    }

    public void InfinityProceduralButton()
    {
        SceneManager.LoadScene("LevelInfinityProcedural");
    }
}
