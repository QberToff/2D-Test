using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    //cashed ref
    Rigidbody2D rb;

    //movement speed
    [SerializeField] float defaultSpeed;
    float speed;

    //score per circle
    float score = 15;


    
    
   
    
    private void Awake()
    {
        defaultSpeed = 5f;
        rb = GetComponent<Rigidbody2D>();
        speed = defaultSpeed;
    }
    
    private void FixedUpdate()
    {
        Move();
    }


    public float GetScore()
    {
        return score;
    }


    private void Move()
    {
        rb.isKinematic = false;
        rb.velocity = new Vector2(0f, -1f * speed) ;
    }

    public void SetSpeed(float acceleration)
    {
        speed += acceleration;
    }

    public void IncreaseDefaultSpeed()
    {
        defaultSpeed += 0.5f;
    }
    public void SetScore(float mult)
    {
        score = score * mult;
    }

}
