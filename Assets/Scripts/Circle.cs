using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    //cashed ref
    Rigidbody2D rb;

    //скорость: defaultSpeed - изначальная, от которой идёт идёт прибавление в speed в зависимости от размера круга
    static float defaultSpeed;
    float speed;

    //количество очков за попадание по кружку
    [SerializeField] float score = 20;

    [SerializeField] AudioClip popSFX;
    [SerializeField] GameObject popFX;

    //длительность партикла при клика на круг
    float durationOfPop = 0.5f; 


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


    private void Move()//метод осущетвляющий движение кружка
    {
        rb.isKinematic = false;
        rb.velocity = new Vector2(0f, -1f * speed) ;
    }

    public void SetSpeed(float acceleration)
    {
        speed += acceleration;
    }

    public static void IncreaseDefaultSpeed()//метод для повышения дефолтной скорости при повышении сложностиыыы
    {
        defaultSpeed += 0.5f;
    }
    public void SetScore(float mult)// метод для установки скорости для экземляра
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

    public void SetPopEffect(ParticleSystem pop)//метод устанавливающий цвет партикла в соответствии со сгенерированным цветом кружка
    {
        var popFX = pop.colorOverLifetime;
        popFX.enabled = true;

        Sprite spr = GetComponent<SpriteRenderer>().sprite;


        Color col = spr.texture.GetPixel(spr.texture.width / 2, spr.texture.width / 2);

         //GetComponent<SpriteRenderer>().color;
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
