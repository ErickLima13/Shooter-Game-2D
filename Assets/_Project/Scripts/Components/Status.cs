using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int life;

    private Animator animator;

    private SpriteRenderer sprite;

    private void Initialization()
    {
        animator = GetComponentInParent<Animator>();
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            Die();
    }

    public void Die()
    {
        animator.SetTrigger("die");
    }

    public void DamageControl(int damage)
    {
        life -= damage;
        StartCoroutine(TakeDamage());
    }

    private IEnumerator TakeDamage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

}
