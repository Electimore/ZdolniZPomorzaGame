using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject rHandItem;
    public GameObject lHandItem;
    public GameObject followTarget;
    public Animator anim;
    public Rigidbody rb;

    RaycastHit lookingAt;
    float speed = 1.5f;
    float moveHorizontal;
    float moveVertical;
    public bool isWalking;
    public bool isRunning;
    bool paused = false;
    bool canAttack = true;
    public bool leftShiftPressed;
    bool canDefend = true;

    private HealthController healthController;
    private HUD hud;
    private GamePauseUI gamePauseUI;
    private GameOverUI gameOverUI;
    private Inventory inventory;
    private Shield shield;
    private GameController gameController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag.Equals("Finish"))
        {
            gameController.EndGame();
        }
    }

    void Start()
    {
        healthController = GetComponent<HealthController>();
        gameOverUI = player.GetComponent<GameOverUI>();
        gamePauseUI = player.GetComponent<GamePauseUI>();
        hud = player.GetComponent<HUD>();
        gameOverUI.SetEnabled(false);
        gamePauseUI.SetEnabled(false);
        inventory = player.GetComponent<Inventory>();
        shield = inventory.shield.GetComponent<Shield>();
        gameController = GameObject.Find("GameMaster").GetComponent<GameController>();
    }

    void Update()
    { 
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (healthController.movable)
        {
            if (moveVertical == 0 && moveHorizontal == 0)
            {
                isWalking = isRunning = false;
            }
            else
            {
                leftShiftPressed = Input.GetKey(KeyCode.LeftShift);
                speed = leftShiftPressed ? 2.5f : 1.5f;
                isWalking = !leftShiftPressed;
                isRunning = leftShiftPressed;
            }
            if (canAttack && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && canDefend)
            {
                shield.RestoreShield();
                anim.SetTrigger("Defend");
                canDefend = false;
                Invoke("DefendCooldown", 10f);
            }
            
            anim.SetBool("IsWalking", isWalking);
            anim.SetBool("IsRunning", isRunning);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Start();
            healthController.Start();
        }

        if (healthController.alive && Input.GetKeyDown(KeyCode.Escape))
        {
            healthController.movable = paused;
            anim.enabled = paused;
            gamePauseUI.SetEnabled(!paused);

            paused = !paused;
        }

        Vector3 forward = followTarget.transform.TransformDirection(Vector3.forward) * 10;

        if (Physics.Raycast(followTarget.transform.position, (forward), out lookingAt))
        {
            if (CheckIfLookingAtSatrtPoint() && !gameController.game)
            {
                lookingAt.collider.GetComponent<ThingyOutliner>().LookingAtObject();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameController.SpawnEnemies();
                    gameController.game = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (healthController.movable)
        {
            player.transform.position += transform.forward * Time.deltaTime * speed * moveVertical;
            player.transform.position += transform.right * Time.deltaTime * speed * moveHorizontal;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        Invoke("AttackCooldown", 1f);
        canAttack = false;

        Vector3 forward = followTarget.transform.TransformDirection(Vector3.forward) * 10;

        if (Physics.Raycast(followTarget.transform.position, (forward), out lookingAt))
        {
            if (CheckIfLookingAtEnemy())
            {
                float distanceFromEnemy = Vector3.Distance(player.transform.position, lookingAt.collider.gameObject.transform.position);
                if(distanceFromEnemy <= 1f)
                {
                    lookingAt.collider.gameObject
                        .GetComponent<EnemyHealthController>()
                        .DealDamage(inventory.GetCurrentWeapon().damage);
                }
            }
        }
    }

    void AttackCooldown()
    {
        canAttack = true;
    }

    void DefendCooldown()
    {
        canDefend = true;
    }

    private bool CheckIfLookingAtEnemy()
    {
        return lookingAt.collider.gameObject.CompareTag("enemy");
    }

    private bool CheckIfLookingAtSatrtPoint()
    {
        return lookingAt.collider.gameObject.CompareTag("LevelStart");
    }

    public void OnShieldPickup()
    {
        shield = inventory.shield.GetComponent<Shield>();
    }
}
