using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullet
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override  void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Status status))
        {
            collision.gameObject.GetComponent<Status>().DamageControl(damage);
            StartCoroutine(Explosion());
        }
    }
}
