using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;
    public Transform floor;
    public Collision collision;

    private void Start()
    {
        floor = GameObject.Find("StartPlatform").GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            FindObjectOfType<AudioManager>().Play("BoxHit");
            Debug.Log("We hit in obstacle!");
            movement.enabled = false;
            FindObjectOfType<GameManager3>().EndGame();
        }

        if (collision.collider.tag == "Floor")
        {
            this.collision = collision;
            floor = collision.transform;
        }
    }
}
