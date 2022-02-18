using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    None,
    Soft,
    Hard
}

public class Player : MonoBehaviour
{
    [Header("Ground Properties")]
    public LayerMask groundLayer;
    public float groundDistance;
    public bool isGrounded;
    public Vector3[] footOffset;

    private int direction = 1;

    [SerializeField] [Range(0, 10)] private float speed = 4f;
    [SerializeField] [Range(0, 10)] private float jumpForce = 7f;
    private float xVelocity;
    private float originalXScale;

    private bool isfire;

    private Rigidbody2D rb2d;

    private Animator animator;

    private Collider2D col;

    [SerializeField] private AudioCharacter audioPlayer = null;

    private Vector2 movement;

    private RaycastHit2D leftCheck;
    private RaycastHit2D rightCheck;

    private weapon weapon;

    private LayerMask softGround;
    private LayerMask hardGround;
    private GroundType groundType;
    

    private void Initialization()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        weapon = GetComponentInChildren<weapon>();
        originalXScale = transform.localScale.x;
        softGround = LayerMask.GetMask("Ground");
        hardGround = LayerMask.GetMask("GroundHard");
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        PhysicsCheck();
        Jump();
        Shoot();

        if (xVelocity * direction < 0)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Movement();
        UpdateGround();
        if (isGrounded)
            audioPlayer.PlaySteps(groundType, Mathf.Abs(xVelocity));
    }

    private void LateUpdate()
    {
        //if ()
        //{
            //animator.SetTrigger("die");
        //    return;
        //}


        animator.SetFloat("xVelocity", Mathf.Abs(xVelocity));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb2d.velocity.y);

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("fire"))
        {
            isfire = true;
        }
        else
        {
            isfire = false;
        }
    }

    private void GetInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontal, 0);
    }

    private void Movement()
    {
        xVelocity = movement.normalized.x * speed;
        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && !isfire)
        {
            animator.SetTrigger("fire");
        }

        if(Mathf.Abs(rb2d.velocity.x) > 0 && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("fireRun");
        }

        if(!isGrounded && Input.GetButtonDown("Fire1") && !isfire)
        {
            animator.SetTrigger("jumpFire");
        }
    }

    public void Shooted()
    {
        if(weapon != null)
        {
            weapon.Shoot();
        }
    }

    private void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x = originalXScale * direction;
        transform.localScale = scale;
    }

    private void UpdateGround()
    {
        if (col.IsTouchingLayers(softGround))
            groundType = GroundType.Soft;
        else if (col.IsTouchingLayers(hardGround))
            groundType = GroundType.Hard;
        else
            groundType = GroundType.None;
    }

    private void PhysicsCheck()
    {
        isGrounded = false;
        leftCheck = Raycast(new Vector2(footOffset[0].x, footOffset[0].y), Vector2.down, groundDistance, groundLayer);
        rightCheck = Raycast(new Vector2(footOffset[1].x, footOffset[1].y), Vector2.down, groundDistance, groundLayer);

        if(leftCheck || rightCheck)
        {
            isGrounded = true;
        }
    }

    private RaycastHit2D Raycast(Vector3 origin,Vector2 rayDirection,float length,LayerMask mask)
    {
        Vector3 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + origin, rayDirection, length, mask);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos + origin, rayDirection * length, color);
        return hit;
    }
}
