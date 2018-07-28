using UnityEngine;

public class PlayerPlatform : MonoBehaviour {

    public Transform player;
    public float distanceBetweenGroungAndPlayer = 0.01f;

    public PlayerCollision playerCollision;

    float lastX, lastY = 0.01f, lastZ;

    private void FixedUpdate()
    {
        transform.position = PlayerPlatformPosition();
    }

    float XOnePlatform()
    {
        float x = player.position.x;

        x = (x < -7) ? -7 : player.position.x;

        x = (x > 7) ? 7 : x;

        return x;
    }

    Vector3 PlayerPlatformPosition()
    {
        float x = lastX, y = lastY, z = lastZ;
        

        if (!(playerCollision.floor.Equals(null)) && !(playerCollision.collision.gameObject.Equals(null)))
        {
            x = player.position.x;
            x = (x < playerCollision.floor.position.x + (-playerCollision.floor.lossyScale.x / 2) + 0.5f) ? playerCollision.floor.position.x + (-playerCollision.floor.lossyScale.x / 2) + 0.5f : x;
            x = (x > playerCollision.floor.position.x + (playerCollision.floor.lossyScale.x / 2) - 0.5f) ? playerCollision.floor.position.x + (playerCollision.floor.lossyScale.x / 2) - 0.5f : x;
            lastX = x;

            z = player.position.z;
            z = (z > playerCollision.floor.position.z + (playerCollision.floor.lossyScale.z / 2) - 0.5f) ? playerCollision.floor.position.z + (playerCollision.floor.lossyScale.z / 2) - 0.5f : z;
            lastZ = z;
        }
        

        

        return new Vector3(x,distanceBetweenGroungAndPlayer,z);
    }
    
}
