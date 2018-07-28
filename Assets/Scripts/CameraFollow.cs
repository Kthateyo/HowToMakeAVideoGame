using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //GENERALY
    public Transform player;
    float currentSmooth;
    bool isJumping;

    //GAME
    ///////////////////////////////////
    public Vector3 offset;
    public Quaternion cameraQuaternion;
    public float smooth;

    //Jump
    public Vector3 jumpOffset;
    public Quaternion jumpQuaternion;
    

    ///////////////////////////////////
    //MENU
    public Vector3 offsetMenu;
    public Quaternion rotationMenu;
    public float smoothAtStart;
    float velocity;
    float progress;
    public float timeProgress = 2f;
    
    void Start () {
        transform.position = player.position + offset;
	}
	
	
	void FixedUpdate ()
    {
        transform.position = Vector3.Lerp(player.position + offset, transform.position, smooth);

        Quaternion quaternion = Quaternion.Euler(cameraQuaternion.x, cameraQuaternion.y, cameraQuaternion.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, smooth);

        if(GameManager3.gameHasStarted == true)
        {
            GetComponent<Animator>().SetBool("NotStarted", false);
            GetComponent<Animator>().SetTrigger("GameStarted");
        }
        else
        {
            GetComponent<Animator>().SetBool("NotStarted", true);
        }

        if(GameManager3.gameHasEnded)
        {
            GetComponent<Animator>().SetTrigger("GameEnded");
        }
        
        if(player.position.y > 2.30f && isJumping == false)
        {
            isJumping = true;
            GetComponent<Animator>().SetTrigger("Jump");
        }

        if(player.position.y < 2.30f && isJumping == true)
        {
            isJumping = false;
            GetComponent<Animator>().SetTrigger("Land");
        }
    }
}
