using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: fix convention
    public GameObject player;
    public GameObject rHandItem;
    public GameObject lHandItem;
    public Animator anim;
    public Rigidbody rb;

    RaycastHit lookingAt;
    float speed = 1.5f;
    float moveHorizontal;
    float moveVertical;
    bool isWalking;
    bool isRunning;
    bool paused = false;

    private HealthController healthController;
    private HUD hud;
    private GamePauseUI gamePauseUI;
    private GameOverUI gameOverUI;

    void Start()
    {
        healthController = GetComponent<HealthController>();
        gameOverUI = player.GetComponent<GameOverUI>();
        gamePauseUI = player.GetComponent<GamePauseUI>();
        hud = player.GetComponent<HUD>();
        gameOverUI.SetEnabled(false);
        gamePauseUI.SetEnabled(false);

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
                var leftShiftPressed = Input.GetKey(KeyCode.LeftShift);
                speed = leftShiftPressed ? 2.5f : 1.5f;
                isWalking = !leftShiftPressed;
                isRunning = leftShiftPressed;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                anim.SetTrigger("Defend");
            }
            
            anim.SetBool("IsWalking", isWalking);
            anim.SetBool("IsRunning", isRunning);
        }
        hud.SetHealth(healthController.hp);
        if (Input.GetKeyDown(KeyCode.R))
        {
            Start();
        }

        if (healthController.alive && Input.GetKeyDown(KeyCode.Escape))
        {
            healthController.movable = paused;
            anim.enabled = paused;
            gamePauseUI.SetEnabled(!paused);

            paused = !paused;
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
}
