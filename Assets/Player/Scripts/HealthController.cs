using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public GameObject player;
    public GameObject playerspawner;

    bool paused = false;
    bool attacked;
    public Animator anim;
    public bool alive;
    public bool movable;

    private GameOverUI gameOverUI;


    public void Start()
    {
        anim.Play("DieRecover");
        gameOverUI = player.GetComponent<GameOverUI>();
        hp = 20f;
        maxHp = 20f;
        attacked = false;
        alive = true;
        player.transform.position = playerspawner.transform.position;
        movable = true;
        anim.enabled = true;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "weapon_1" && attacked == false) //1 DMG weapon hit
        {
            hp -= 1;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        attacked = false;
    }

    public void RecieveDamage(float damage)
    {
        hp -= damage;
    }

    private void Update()
    {
        if (hp <= 0.99 && alive) //Checks if player is alive
        {
            anim.SetTrigger("Die");
            alive = false;
            gameOverUI.SetEnabled(true);
            movable = false;
        }
    }
}
