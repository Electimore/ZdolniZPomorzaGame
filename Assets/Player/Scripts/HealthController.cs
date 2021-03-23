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
    private Shield shield;
    private HUD hud;

    public void Start()
    {
        anim.Play("DieRecover");
        gameOverUI = player.GetComponent<GameOverUI>();
        hud = player.GetComponent<HUD>();
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
            attacked = true;
            RecieveDamage(1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        attacked = false;
    }

    public void RecieveDamage(float incomingDamage)
    {
        if(shield.durability <= 0)
        {
            hp -= incomingDamage;
            anim.SetTrigger("Gethit");
        }
        else
        {
            if(shield.durability <= incomingDamage)
            {
                shield.durability = 0;
            }
            else
            {
                shield.durability -= incomingDamage;
            }
        }
        
    }

    private void Update()
    {
        hud.SetHealth(hp, maxHp);

        shield = player.GetComponent<Inventory>().shield.GetComponent<Shield>();
        if (hp <= 0.99 && alive)
        {
            anim.SetTrigger("Die");
            alive = false;
            gameOverUI.SetEnabled(true);
            movable = false;
        }
    }
}
