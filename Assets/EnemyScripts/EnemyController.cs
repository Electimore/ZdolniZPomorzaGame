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
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        Ray ray = new Ray(enemy.transform.position, (player.transform.position - this.transform.position).normalized * 10);

        if(distanceFromPlayer <= 1.1 && canAttack && enemyHealthController.alive && player.GetComponent<HealthController>().movable)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        float chanceToAttack = Random.Range(0f, 10f);
        canAttack = false;
        Invoke("AttackCooldown", 1.5f);
        if (chanceToAttack <= 4 && chanceToAttack >= 1)
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
        //float dropRandom = Random.Range(0f, 10f);
        float dropRandom = 8;
        if(dropRandom >= 7)
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
