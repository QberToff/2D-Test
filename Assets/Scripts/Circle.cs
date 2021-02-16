using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    //cashed ref
    Rigidbody2D rb;

    //movement speed
    static float defaultSpeed;
    float speed;

    //score per circle
    float score = 15;

    [SerializeField] AudioClip popSFX;
    [SerializeField] GameObject popFX;


    float durationOfPop = 1f;


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

    public static void IncreaseDefaultSpeed()
    {
        defaultSpeed += 0.5f;
    }
    public void SetScore(float mult)
    {
        score = score * mult;
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(popSFX, Camera.main.transform.position);
        //PopEffect(randCol);
       GameObject pop = Instantiate(popFX as GameObject, transform.position, Quaternion.identity);
        SetPopEffect(pop.GetComponent<ParticleSystem>());
        Destroy(pop, durationOfPop);
        Destroy(gameObject);
    }

    public void SetPopEffect(ParticleSystem pop)
    {
        var popFX = pop.colorOverLifetime;
        popFX.enabled = true;

        Color col = GetComponent<SpriteRenderer>().color;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] {new GradientColorKey(col, 0f),
             new GradientColorKey(col, 0.5f),
             new GradientColorKey(Color.white, 1f)},
            new GradientAlphaKey[] {new GradientAlphaKey(1f, 0f),
             new GradientAlphaKey(1f, 0.5f),
             new GradientAlphaKey(0f, 1f)});
        popFX.color = grad;
        //popFX.Play();


    }

}
