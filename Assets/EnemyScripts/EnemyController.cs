using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public GameObject rightHand;
    public GameObject leftHand;

    public Animator anim;

    float damageToDeal;
    float distanceFromPlayer;
    bool canAttack;
    bool canHit;
    bool playerNoticed;
    bool refresh;

    private EnemyHealthController enemyHealthController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("enemy");
        enemyHealthController = this.GetComponent<EnemyHealthController>();
        canAttack = true;
    }

    void Update()
    {
        if(player.GetComponent<HealthController>().movable && player.GetComponent<HealthController>().alive)
        {
            distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

            if (distanceFromPlayer <= 5 && distanceFromPlayer >= 1.1)
            {
                playerNoticed = true;
            }
            else
            {
                playerNoticed = false;
            }

            if (playerNoticed == true)
            {
                GetComponent<EnemyMovement>().GoToGameObject(player);
            }

            anim.SetBool("Walking", playerNoticed);

            if (distanceFromPlayer <= 1.1 && canAttack && enemyHealthController.alive && player.GetComponent<HealthController>().movable)
            {
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        float chanceToHit = Random.Range(0f, 5f);
        float chanceToAttack = Random.Range(0f, 10f);

        if (!player.GetComponent<PlayerController>().isWalking && chanceToHit*2 >= 3)
        {
            canHit = true;
        }
        else if(player.GetComponent<PlayerController>().isWalking && chanceToHit >= 3)
        {
            canHit = true;
        }
        else
        {
            canHit = false;
        }
        
        canAttack = false;
        Invoke("AttackCooldown", 2f);
        if (chanceToAttack <= 4 && chanceToAttack >= 1 && canHit)
        {
            anim.SetTrigger("AttackRight");
            Invoke("DealPlayerDamage", 0.5f);
            damageToDeal = rightHand.GetComponentInChildren<Weapon>().damage;
        }
        else if(chanceToAttack <= 8 && chanceToAttack > 4)
        {
            anim.SetTrigger("AttackLeft");
            Invoke("DealPlayerDamage", 0.5f);
            damageToDeal = leftHand.GetComponentInChildren<Weapon>().damage;
        }
        else if (chanceToAttack <= 9 && chanceToAttack > 8)
        {
            anim.SetTrigger("AttackBoth");
            Invoke("DealPlayerDamage", 0.5f);
            damageToDeal = leftHand.GetComponentInChildren<Weapon>().damage + rightHand.GetComponentInChildren<Weapon>().damage;
        }
    }

    void AttackCooldown()
    {
        canAttack = true;
    }

    void DealPlayerDamage()
    {
        player.GetComponent<HealthController>().RecieveDamage(damageToDeal);
    }

    public void DropWeapons()
    {
        int dropRandom = Random.Range(0, 10);
        if(dropRandom >= 6)
        {
            GameObject droppedWeapon = rightHand.transform.GetChild(0).gameObject;
            droppedWeapon.transform.parent = null;
            droppedWeapon.transform.position = this.transform.position;
            droppedWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            droppedWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            droppedWeapon.tag = "weapon";
        }
    }
}
