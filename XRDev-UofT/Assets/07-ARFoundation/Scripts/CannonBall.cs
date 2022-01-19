using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var robot = collision.collider.GetComponent<RobotTouchController>();

        if (robot)
        {
            // destroy the robot and the cannon ball
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
