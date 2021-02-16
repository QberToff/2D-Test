using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //cashed ref for score text
    [SerializeField] Text scoreText;
    
    float earnedScore = 0;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ProcessClick();
        }
    }


    private void ProcessClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.transform != null)
        {
            Debug.Log(hit.transform.name);
            Circle circle = hit.collider.gameObject.GetComponent<Circle>();
           
            if (circle != null)
            {
                circle.Die();
                earnedScore += circle.GetScore();
                scoreText.text = earnedScore.ToString();
            }

            //Destroy(hit.collider.gameObject);


        }
    }


   
}
