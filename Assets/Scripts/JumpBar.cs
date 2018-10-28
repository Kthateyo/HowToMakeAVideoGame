using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour {

    public float Multiplier = 2;

    public Slider bar1, bar2;
    public GameObject text;
    public GameObject Player;
    PlayerMovement playerMovement;

	void Start ()
    {
        text.SetActive(false);
        playerMovement = Player.GetComponent<PlayerMovement>();
        bar1.normalizedValue = 0;
        bar2.normalizedValue = 0;
    }

	void Update ()
    {
        if (bar1.normalizedValue != 1)
        {
            bar1.normalizedValue = bar1.normalizedValue + Multiplier * Time.deltaTime;
            bar2.normalizedValue = bar2.normalizedValue + Multiplier * Time.deltaTime;

            if (bar1.normalizedValue == 1)
                text.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            bar1.normalizedValue = 0;
            bar2.normalizedValue = 0;
            text.SetActive(false);
            playerMovement.Jump();
        }
	}
}
