using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CircleSpawner : MonoBehaviour
{
    //circle cashed reference and object for configure spawning circle
    Circle circle;
    Circle nextCircle;

    //cashed ref for bundleloader
    [SerializeField] BundleLoader bundleLoader;

    
    float timeBetweenSpawns = 1f;
    
    Vector3 spawnPos;// переменная, хранящая положение самого спаунера

    int circleCounter = 0;//счётчик созданных кругов
    int diffcounter = 0;//число кругов при котором изменится уровень сложнсти
    int difflevel = 1;
    bool finalDifficulty = false; //индикатор финальной сложности

    [SerializeField] TextMeshProUGUI level;// cashed ref to UI level

    [SerializeField] int changeEachCircle = 25;// количество созданных кругов для перехода на следующий уровень слолжности

    //границы игрового пространства
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 1.7f;

    //границы для генерации множителя размера
    float minScale = 0.7f;
    float maxScale = 3.5f;

    bool looping =  true;
    bool isChecked = false;

    


    private void Awake()
    {
        spawnPos = transform.position;
        SetUpMoveBoudaries();
        diffcounter = changeEachCircle;
        bundleLoader = FindObjectOfType<BundleLoader>();
        difflevel = 1;
        level.text = difflevel.ToString();
        

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
        GetCirclePrefab();
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
        if(circle != null)
        {
            GenerateCircle();
            Debug.Log("Circle spawned");
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        
    }

    private void GenerateCircle()//метод генерации круга
    {
        float scaleFactor = Random.Range(minScale, maxScale);//генерация размера для круга 
        nextCircle = Instantiate(circle as Circle, new Vector3(Random.Range(xMin, xMax), spawnPos.y, spawnPos.z), Quaternion.identity);//инстант круга
        nextCircle.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);//изменение размера круга
        Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);//генерация цвета круга
        nextCircle.GetComponent<SpriteRenderer>().color = col;//присвоение сгенерированного цвета
        circleCounter++;
        
        //условия, отвечающие за установку скорости и количества очков в зависимости от размера
        if (scaleFactor >= minScale && scaleFactor <= 1f)
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

    private void ProcessDifficulty()//увеличение уровня сложности, посредством уменьшения времени между спауном
    {
        
        if(circleCounter == diffcounter && !finalDifficulty)
        {
            timeBetweenSpawns -= 0.1f;
            Circle.IncreaseDefaultSpeed();
            if (timeBetweenSpawns <= 0.2f)
            {
                finalDifficulty = true;
            }
            difflevel++;
            diffcounter += changeEachCircle;
            level.text = difflevel.ToString();
            Debug.Log(difflevel);

        }
        
    }

    private void GetCirclePrefab()
    {
        if(!isChecked)
        {
            if(bundleLoader.IsLoaded)
            {
                circle = bundleLoader.GetLoadedAsset().GetComponent<Circle>();
                isChecked = true;
            }


        }



    }

}
