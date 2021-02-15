using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    //circle cashed reference and object for configure spawning circle
    [SerializeField] Circle circle;
    Circle nextCircle;
    
    float timeBetweenSpawns = 1f;
    
    Vector3 spawnPos;

    int circleCounter = 0;
    int diffcounter = 0;
    [SerializeField] int changeEachCircle = 5;
    bool finalDifficulty = false;

    //playspace borders
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 1.7f;

    //scale restrictions
    float minScale = 0.7f;
    float maxScale = 3.5f;

    bool looping =  true;
    //bool diff = false;
    //bool isProcessing = false;

    


    private void Awake()
    {
        spawnPos = transform.position;
        SetUpMoveBoudaries();
        diffcounter = changeEachCircle;
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
        ProcessDifficulty();
    }


    private void SetUpMoveBoudaries()//установка границ игрового поля
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
        nextCircle.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        circleCounter++;
        
        if (scaleFactor > minScale && scaleFactor <= 1f)
        {
            nextCircle.SetSpeed(5.5f);
            nextCircle.SetScore(4f);
        }
        else if (scaleFactor > 1f && scaleFactor <= 1.5f)
        {
            nextCircle.SetSpeed(4.5f);
            nextCircle.SetScore(3.5f);
        }
        else if (scaleFactor > 1.5f && scaleFactor <= 2f)
        {
            nextCircle.SetSpeed(3.5f);
            nextCircle.SetScore(3f);
        }
        else if (scaleFactor >2f && scaleFactor <= 2.5f)
        {
            nextCircle.SetSpeed(2.5f);
            nextCircle.SetScore(2.5f);
        }
        else if (scaleFactor > 2f && scaleFactor <= 2.5f)
        {
            nextCircle.SetSpeed(1.5f);
            nextCircle.SetScore(2.5f);
        }
        else if (scaleFactor > 2.5f && scaleFactor <= 3f)
        {
            nextCircle.SetSpeed(0.5f);
            nextCircle.SetScore(1.5f);
        }

    }

    private void ProcessDifficulty()
    {
        
        if(circleCounter == diffcounter && !finalDifficulty)
        {
            timeBetweenSpawns -= 0.1f;
            circle.IncreaseDefaultSpeed();
            if(timeBetweenSpawns <= 0.2f)
            {
                finalDifficulty = true;
            }
            diffcounter += changeEachCircle;
        }
        
        
        /*(float division = circleCounter % 5;
        if(division == 0 && !diff)
        {
            diff = true;
            isProcessing = true;
        }  */  

    }
}
