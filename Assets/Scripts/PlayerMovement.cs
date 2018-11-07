using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;

    public float jump = 50f;
    public float ForwardStartForce = 6000;
    public float forwardForce = 1000;
    public float maxforwardForce = 1000;
    public float sidewaysForce = 1000;
    public float downForce = 1000;

    public bool acceleration = true;
    public float speedOfAcceleration = 2;

    Rigidbody rigidbody;
    float forwardForceAtStart = 0;
    float velocity;

    ////////////////////////////////////

    Vector3 vectorAtDash;
    bool afterDash;
    public float maxTimeBetweenClicks = 0.1f;
    public float whenStopDash = 3;
    public float sidewaysDash = 1000;

    ////////////////////////////////////

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    void Movement()
    {
        if (GameManager3.gameHasStarted == true)
        {
            anim.enabled = false;
            rigidbody.useGravity = true;

            if (Time.fixedTime < GameManager3.gameStartedTime + 6)
            {
                forwardForceAtStart = Mathf.SmoothDamp(forwardForceAtStart, forwardForce, ref velocity, 3f);

                rigidbody.AddForce(0, 0, forwardForceAtStart * Time.deltaTime);
            }
            else
            {
                rigidbody.AddForce(0, 0, forwardForce * Time.deltaTime);
            }

            if (acceleration == true)
            {
                if (forwardForce <= maxforwardForce)
                {
                    forwardForce = forwardForce + (speedOfAcceleration * Time.deltaTime);
                }
            }

            ///////////////////////////
            
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }

            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Down();
            }

            if (rigidbody.position.y < -1)
            {
                FindObjectOfType<GameManager3>().EndGame();
                enabled = false;
            }
        }

        else
        {
            anim.enabled = true;
            rigidbody.useGravity = false;
        }
    }

    private void Down()
    {
        rigidbody.AddForce(0, -downForce * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    public void Jump()
    {
        rigidbody.AddForce(0, jump, 0, ForceMode.Impulse);
    }

    void MoveLeft()
    {
        rigidbody.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    void MoveRight()
    {
        rigidbody.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    public void RandomRotatePlayer(float min, float max, ForceMode mode)
    {
        rigidbody.AddTorque(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max), mode);
    }
}


