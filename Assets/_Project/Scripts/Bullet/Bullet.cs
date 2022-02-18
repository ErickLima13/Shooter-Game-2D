using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public Vector2 direction;

    [SerializeField] [Range(0, 10)] private float speed;

    public GameObject explosion;

    protected Animator animator;

    protected SpriteRenderer _renderer;

    [SerializeField] [Range(0, 10)] protected float livingTime = 3f;

    protected Rigidbody2D rb;


    protected virtual void Initiazalition()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = explosion.GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Awake()
    {
        Initiazalition();
    }

    protected virtual void FixedUpdate()
    {
        Movement();

        livingTime -= Time.deltaTime;
        if(livingTime < 0)
        {
            gameObject.SetActive(false);
            livingTime = 2f;
        }
    }

    protected virtual void Movement()
    {
        Vector2 movement = direction.normalized * speed;
        rb.velocity = movement;
    }

    public IEnumerator Explosion()
    {
        speed = 0;
        _renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        explosion.SetActive(true);
 
        yield return new WaitForSeconds(0.1f);

        speed = 10;
        gameObject.SetActive(false);
        _renderer.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        explosion.SetActive(false);
    }
}
