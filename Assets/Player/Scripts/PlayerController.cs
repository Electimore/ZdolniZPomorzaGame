using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject r_hand;
    public GameObject l_hand;
    public GameObject r_hand_item;
    public GameObject l_hand_item;
    public Rigidbody rb;
    //public Camera cam;
    public float speed = 1.5f;
    public float sense = 750f;
    float temp_speed;
    float temp_speed_2;
    public Animator anim;
    Vector3 jump;
    float jumpForce = 2.0f;
    bool isGrounded;
    bool isWalking;
    bool isRunning;
    Health_controller hp_con;
    RaycastHit lookingAt;
    float distance;
    Vector3 forward;
    public GameObject followTarget;


    void OnMouseOver()
    {
        Debug.Log("The player is looking at " + transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        jump = new Vector3(0.0f, 2.0f, -0.0f);
        hp_con = GetComponent<Health_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        /*float rotateHorizontal = -Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");*/

        if (hp_con.movable)
        {
            //player.transform.Rotate(new Vector3(0f, -rotateHorizontal, 0f) * Time.deltaTime * sense); //LOOK R-L

            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            {
                isWalking = false;
                isWalking = false;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2.5f;
                isRunning = true;
                isWalking = false;
            }
            else
            {
                speed = 1.5f;
                isWalking = true;
                isRunning = false;
            }

            //ATTACC
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("Attacc");
            }

            //DEFEND
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                anim.SetTrigger("Defend");
            }

            /* old cam movement //LOOK UP-DOWN + LOCK
            if (cam.transform.rotation.eulerAngles.x < 45 && (cam.transform.rotation.eulerAngles.x) >= 0)
            {
                cam.transform.Rotate(new Vector3(-rotateVertical, 0f, 0f) * Time.deltaTime * sense);
            }
            else if (cam.transform.rotation.eulerAngles.x > 45 && (cam.transform.rotation.eulerAngles.x) < 180)
            {
                cam.transform.Rotate(new Vector3(-0.1f, 0f, 0f) * Time.deltaTime * sense);
            }
            if (cam.transform.rotation.eulerAngles.x < 360 && (cam.transform.rotation.eulerAngles.x) > 315)
            {
                cam.transform.Rotate(new Vector3(-rotateVertical, 0f, 0f) * Time.deltaTime * sense);
                Debug.Log(cam.transform.rotation.eulerAngles.x);
            }
            else if (cam.transform.rotation.eulerAngles.x < 315 && (cam.transform.rotation.eulerAngles.x) >= 180)
            {
                cam.transform.Rotate(new Vector3(0.1f, 0f, 0f) * Time.deltaTime * sense);
            */

            player.transform.position += transform.forward * Time.deltaTime * speed * moveVertical;
            player.transform.position += transform.right * Time.deltaTime * speed * moveHorizontal;
            

            //ANIMATOR_THINGS
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

    void OnCollisionEnter()
    {
        isGrounded = true;
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
