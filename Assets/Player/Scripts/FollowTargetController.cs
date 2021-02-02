using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class FollowTargetController : MonoBehaviour
{
    public float sense = -1f;
    public Vector2 _look;
    public Vector2 _move;
    public GameObject followTransform;

    Health_controller hp_con;


    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hp_con = GetComponent<Health_controller>();
    }

    private void Update()
    {
        if (hp_con.movable)
        {
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * sense, Vector3.right);
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * sense, Vector3.up);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 30)
            {
                angles.x = 30;
            }

            followTransform.transform.localEulerAngles = angles;

            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
}
