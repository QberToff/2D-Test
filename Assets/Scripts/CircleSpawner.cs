using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    //circle cashed reference and object for configure spawning circle
    [SerializeField] Circle circle;
    Circle nextCircle;
    
    [SerializeField] float timeBetweenSpawns = 0.2f;
    
    Vector3 spawnPos;


    //playspace borders
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 1.7f;

    //scale restrictions
    float minScale = 0.7f;
    float maxScale = 3.5f;

    [SerializeField] bool looping =  true;

    // Start is called before the first frame update

    private void Awake()
    {
        spawnPos = transform.position;
        SetUpMoveBoudaries();
    }

    IEnumerator Start()
    {
        do
        {
           yield return StartCoroutine(SpawnCircles());
        }
        while (looping);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetUpMoveBoudaries()//set borders
    {

        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    IEnumerator SpawnCircles()
    {
        GenerateCircle();
        Debug.Log("Circle spawned");
        yield return new WaitForSeconds(timeBetweenSpawns);
    }

    private void GenerateCircle()
    {
        float scaleFactor = Random.Range(minScale, maxScale);
        nextCircle = Instantiate(circle as Circle, new Vector3(Random.Range(xMin, xMax), spawnPos.y, spawnPos.z), Quaternion.identity);
        nextCircle.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        
        
        if (scaleFactor > minScale && scaleFactor <= 1.2f)
        {
            nextCircle.SetSpeed(5f);
            nextCircle.SetScore(3f);
        }
        else if (scaleFactor > 1.2f && scaleFactor < 2.5f)
        {
            nextCircle.SetSpeed(2.5f);
            nextCircle.SetScore(1.5f);
        }

    }
}
