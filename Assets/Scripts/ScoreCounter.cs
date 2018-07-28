using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public Transform player;
    public float multiply;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update ()
    {
        int score = (int) (player.position.z * multiply);
        text.text = score.ToString();


    }
}
