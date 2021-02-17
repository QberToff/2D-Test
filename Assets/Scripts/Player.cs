using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    //cashed ref for score text
    [SerializeField] TextMeshProUGUI scoreText;

    //cashed ref for timer text
    [SerializeField] TextMeshProUGUI timerText;
    
    //количество заработанных очков
    float earnedScore = 0;

    //переменные для таймера
    float startTime;// начальное время
    float currentTime; //текущее время 

    private void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startTime;//вычисление текущего времени для таймера
        string min = ((int) currentTime / 60).ToString();//подсчёт минут для таймера
        string sec = (currentTime % 60).ToString("f1");//подсчёт секунд для таймера

        timerText.text = min + ":" + sec;//формирование таймера

        if (Input.GetMouseButtonDown(0))
        {
            ProcessClick();
        }
    }


    private void ProcessClick()// метод обрабатывающий клик игрока
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.transform != null)
        {
            Debug.Log(hit.transform.name);
            Circle circle = hit.collider.gameObject.GetComponent<Circle>();
           
            if (circle != null)//взаимодействие с нажатым кружком
            {
                circle.Die();
                earnedScore += Mathf.Round(circle.GetScore());//получение очков за кружок с округлением до целого числа
                scoreText.text = earnedScore.ToString();//передача полученного значения в UI
                
            }

            //Destroy(hit.collider.gameObject);


        }
    }


   
}
