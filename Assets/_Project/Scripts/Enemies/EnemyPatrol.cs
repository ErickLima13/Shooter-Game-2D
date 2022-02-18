using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy
{
    [SerializeField] [Range(0,10)] private float timerWaitAtttack;
    [SerializeField] [Range(0, 10)] private float timerShootAttack;

    private bool idle;
    private bool shoot;

    protected override void Awake()
    {
        base.Awake();
    }


    private void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
        if (!RaycastGround().collider || RaycastWall().collider)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {

        if (CanAttack())
        {
            Attack();
        }
        else
        {
            Movement();
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("idle", idle);
    }

    private void Movement()
    {
        float horizontalVelocity = speed;
        horizontalVelocity = horizontalVelocity * direction;
        //rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        rb.MovePosition(rb.position + new Vector2(horizontalVelocity, 0) * Time.deltaTime);
        idle = false;
    }

    private bool CanAttack()
    {
        return RaycastPlayer().collider;
    }

    private void Attack()
    {
        StopMovement();
        DistancieFlipPlayer();
        CanShoot();
    }

    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
        idle = true;
    }

    private void DistancieFlipPlayer()
    {
        if(playerDistance >= 0 && direction == -1)
        {
            Flip();
        }
        else if(playerDistance < 0 && direction == 1)
        {
            Flip();
        }
    }

    private void CanShoot()
    {
        if (!shoot)
        {
            StartCoroutine("Shoot");
        }
    }

    public void Shooter()
    {
        if (weapon != null)
        {
            weapon.ShootEnemy();
        }
    }

    private IEnumerator Shoot()
    {
        shoot = true;
        yield return new WaitForSeconds(timerWaitAtttack);
        AnimationShoot();
        yield return new WaitForSeconds(timerShootAttack);
        shoot = false;
    }

    private void AnimationShoot()
    {
        animator.SetTrigger("shoot");
    }

    private void OnDisable()
    {
        Destroy(gameObject, 2f);
    }
}
