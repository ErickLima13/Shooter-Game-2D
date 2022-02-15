using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;

    [SerializeField] [Range(0, 10)] private float speed;

    public GameObject explosion;

    protected Animator animator;

    [SerializeField] [Range(0, 10)] protected float livingTime = 3f;

    protected Rigidbody2D rb;


    protected virtual void Initiazalition()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = explosion.GetComponent<Animator>();
    }

    protected virtual void Awake()
    {
        Initiazalition();
    }

    protected virtual void Start()
    {
        //Destroy(gameObject, livingTime);
    }

    protected virtual void FixedUpdate()
    {
        Movement();

        livingTime -= Time.deltaTime;
        if(livingTime < 0)
        {
            gameObject.SetActive(false);
            livingTime = 3f;
        }
    }


    protected virtual void Movement()
    {
        Vector2 movement = direction.normalized * speed;
        rb.velocity = movement;
    }

    public void Explode()
    {
        speed = 0f;
        GetComponent<BoxCollider2D>().enabled = false;
        if(explosion != null)
        {
            explosion.SetActive(true);
        }
        //Destroy(gameObject, 1.5f);
        gameObject.SetActive(false);
    }
}
