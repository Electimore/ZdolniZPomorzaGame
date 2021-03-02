using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float health;
    public bool alive;

    public Animator anim;

    void Start()
    {
        health = 5;
        alive = true;
    }

public void DealDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            alive = false;
            Invoke("Die", 0.5f);
            this.GetComponent<EnemyController>().DropWeapons();
        }
        Invoke("TakeDamage", 0.5f);
    }

    private void TakeDamage()
    {
        anim.SetTrigger("TakingDamage");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        Invoke("Disappear", 1.5f);
    }

    public void Disappear()
    {
        Destroy(this.gameObject);
    }
}
