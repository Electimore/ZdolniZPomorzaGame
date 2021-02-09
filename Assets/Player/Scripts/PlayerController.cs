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
    
    private HealthController healthController;

    void Start()
    {
        healthController = GetComponent<HealthController>();
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
    }

    private void FixedUpdate()
    {
        player.transform.position += transform.forward * Time.deltaTime * speed * moveVertical;
        player.transform.position += transform.right * Time.deltaTime * speed * moveHorizontal;
    }
}
