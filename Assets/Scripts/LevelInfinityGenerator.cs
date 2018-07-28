using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfinityGenerator : MonoBehaviour
{
    //////////////
    /// Generally
    //////////////
    public float generateDistance = 180;
    public int countOfObstacles = 10;

    public Transform player;

    Vector3 endOfLastPlatform;
    Vector3 pointOfGenerate;

    //////////////
    /// Random Generating
    //////////////
    public bool randomGenerating = false;

    public GameObject platform;
    public GameObject obstacle;

    List<GameObject> obstacles = new List<GameObject>();
    List<GameObject> platforms = new List<GameObject>();

    //////////////
    /// Procedural Generating
    //////////////
    public bool proceduralGenerating = false;

    public GameObject[] proceduralPlatforms;

    int previousIndex = 0;


    void Start()
    {
        endOfLastPlatform = GameObject.Find("StartPlatform").transform.position + (new Vector3 (0,0,GameObject.Find("StartPlatform").transform.lossyScale.z / 2));
        
        platforms.Add(GameObject.FindGameObjectWithTag("Floor"));
    }

    void Update()
    {
        if (endOfLastPlatform.z < player.position.z + generateDistance)
        {
            if (randomGenerating)
            {
                GeneratePlatform();
                GenerateObstacleRandom(pointOfGenerate, countOfObstacles);
                DeleteRandoms();
            }

            if (proceduralGenerating)
            {
                GenerateProcedural();
                DeleteProcedural();
            }
        }
    }

    //////////////
    /// Random Generating
    //////////////

    void GeneratePlatform()
    {
        pointOfGenerate = endOfLastPlatform + new Vector3(0, 0, platform.transform.lossyScale.z / 2);

        platforms.Add(Instantiate(platform, pointOfGenerate, new Quaternion(0, 0, 0, 0)));
        //Instantiate(platform, pointOfGenerate, new Quaternion(0, 0, 0, 0));

        endOfLastPlatform = endOfLastPlatform + new Vector3(0, 0, platform.transform.lossyScale.z);
    }

    void GenerateObstacleRandom(Vector3 vector, int x)
    {
        for (int i = 0; i < x; i++)
        {
            //Instantiate(obstacle, (vector + new Vector3(Random.Range(-platform.transform.lossyScale.x/2, platform.transform.lossyScale.x / 2), 1,
            //    Random.Range(-platform.transform.lossyScale.x / 2, platform.transform.lossyScale.x / 2))), new Quaternion(0, Random.Range(0f, 360f), 0, 360));

            obstacles.Add(Instantiate(obstacle, (vector + new Vector3(Random.Range(-platform.transform.lossyScale.x / 2, platform.transform.lossyScale.x / 2), 1,
                Random.Range(-platform.transform.lossyScale.x / 2, platform.transform.lossyScale.x / 2))), new Quaternion(0, Random.Range(0f, 360f), 0, 360)));
        }
    }

    void DeleteRandoms()
    {
        if (platforms[0].transform.position.z < player.position.z)
        {
            Destroy(platforms[0], 0.5f);
            platforms.RemoveAt(0);
        }

        for (int i = 0; i < countOfObstacles; i++)
        {
            if (obstacles[0].transform.position.z < player.position.z)
            {
                Destroy(obstacles[0], 0.5f);
                obstacles.RemoveAt(0);
            }
        }
    }

    //////////////
    /// Procedural Generating
    //////////////

    void GenerateProcedural()
    {
        int indexToGenerate = Random.Range(0, proceduralPlatforms.Length);

        //Generate first Entry //GameManager3.gameStartedTime + 5 > Time.time
        while (proceduralPlatforms[previousIndex].tag == "OneWayPlatform" && (proceduralPlatforms[indexToGenerate].tag == "DoublePlatformExit" || proceduralPlatforms[indexToGenerate].tag == "DoublePlatform"))
        {
            indexToGenerate = Random.Range(0, proceduralPlatforms.Length);
        }

        while((proceduralPlatforms[previousIndex].tag == "DoublePlatformEntry" || proceduralPlatforms[previousIndex].tag == "DoublePlatform") && (proceduralPlatforms[indexToGenerate].tag == "OneWayPlatform" || proceduralPlatforms[indexToGenerate].tag == "DoublePlatformEntry"))
        {
            indexToGenerate = Random.Range(0, proceduralPlatforms.Length);
        }

        while(proceduralPlatforms[previousIndex].tag == "DoublePlatformExit" && (proceduralPlatforms[indexToGenerate].tag == "DoublePlatform" || proceduralPlatforms[indexToGenerate].tag == "DoublePlatformExit"))
        {
            indexToGenerate = Random.Range(0, proceduralPlatforms.Length);
        }

        //Generate F
        /*
        Generate next DoublePlatform or DoublePlatformExit
        while (proceduralPlatforms[previousIndex].tag == "DoublePlatform" || proceduralPlatforms[previousIndex].tag != "DoublePlatformEntry" &&
            (proceduralPlatforms[indexToGenerate].tag != "DoublePlatform" || proceduralPlatforms[indexToGenerate].tag != "DoublePlatformExit"))
        {
            indexToGenerate = Random.Range(0, proceduralPlatforms.Length);
        }*/

        
        previousIndex = indexToGenerate;
        
        pointOfGenerate = endOfLastPlatform + new Vector3(0, 0, proceduralPlatforms[indexToGenerate].transform.Find("BasePlatform").lossyScale.z / 2);

        platforms.Add(Instantiate(proceduralPlatforms[indexToGenerate], pointOfGenerate, new Quaternion(0, 0, 0, 0)));

        endOfLastPlatform = endOfLastPlatform + new Vector3(0, 0, proceduralPlatforms[indexToGenerate].transform.Find("BasePlatform").lossyScale.z);
    }

    void DeleteProcedural()
    {
        if (platforms[0].transform.position.z < player.position.z - 100)
        {
            Destroy(platforms[0], 0.5f);
            platforms.RemoveAt(0);
        }
    }
}