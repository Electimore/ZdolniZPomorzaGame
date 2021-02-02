using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: fix convention
    public GameObject player;
    public GameObject r_hand;
    public GameObject l_hand;
    public GameObject r_hand_item;
    public GameObject l_hand_item;
    public Rigidbody rb;
    public float speed = 1.5f;
    public float sense = 750f;
    public Animator anim;
    bool isWalking;
    bool isRunning;
    private HealthController healthController;
    RaycastHit lookingAt;
    float distance;
    Vector3 forward;
    public GameObject followTarget;


    void OnMouseOver()
    {
        Debug.Log("The player is looking at " + transform.name);
    }

    void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (healthController.movable)
        {
            //PLAYER_INPUT
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

            //PLAYER_MOVEMENT
            player.transform.position += transform.forward * Time.deltaTime * speed * moveVertical;
            player.transform.position += transform.right * Time.deltaTime * speed * moveHorizontal;

            //ATTACC
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("Attack");
            }

            //DEFEND
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                anim.SetTrigger("Defend");
            }
            
            //ANIMATOR_CONTROLL
            anim.SetBool("IsWalking", isWalking);
            anim.SetBool("IsRunning", isRunning);

            forward = followTarget.transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(followTarget.transform.position, forward, Color.blue);

            if (Physics.Raycast(followTarget.transform.position, (forward), out lookingAt))
            {
                distance = lookingAt.distance;
                print(distance + " " + lookingAt.collider.gameObject.name);
                if (GameObject.Find(lookingAt.collider.gameObject.name).CompareTag("weapon_1") == true)
                {
                    GameObject.Find(lookingAt.collider.gameObject.name).GetComponent<ThingyOutliner>().LookingAtObject();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickRightHand(lookingAt.collider.gameObject.name);
                    }
                }
            }
        }
    }

    void pickRightHand(string name)
    {
        GameObject switchToWeapon = lookingAt.collider.gameObject;
        GameObject oldWeapon = GameObject.FindGameObjectWithTag("weapon_picked");
        GameObject wpSlot = GameObject.Find("Weapon");
        oldWeapon.tag = null;
        GameObject newWeapon = Instantiate(switchToWeapon, wpSlot.transform.position, new Quaternion(0, 0, 0, 0));
        newWeapon.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        newWeapon.transform.parent = wpSlot.transform;
        newWeapon.tag = "weapon_picked";
        Destroy(oldWeapon);
        Destroy(switchToWeapon);
    }

}
