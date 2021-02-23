using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float health;

    public Animator anim;

    void Start()
    {
        health = 5;
    }


    void Update()
    {
        
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        anim.SetTrigger("TakingDamage");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        Invoke("Disappear", 1.5f);
    }

    public void Disappear()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
