using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTouchController : MonoBehaviour
{
    public float moveSpeed = 30f;
    public float turnSpeed = 5f;
    public float deadZone = 0.2f;

    private Animator robotAnim;
    private Rigidbody robotRigidbody;
    private Joystick joystick;

    void OnEnable()
    {
        joystick = FindObjectOfType<Joystick>();
        robotRigidbody = GetComponent<Rigidbody>();
        robotAnim = GetComponent<Animator>();

        robotAnim.SetBool("Open Anim", true);
    }

 
    void Update()
    {
        // handle movement
        if (joystick.Direction.magnitude >= deadZone)
        {
            robotRigidbody.AddForce(transform.forward * moveSpeed);

            // set the robot animator to walking
            robotAnim.SetBool("Walk Anim", true);
        }
        else
        {
            robotAnim.SetBool("Walk Anim", false);
        }

        //  handle rotation
        Vector3 targetDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * turnSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(direction);

    }
}
