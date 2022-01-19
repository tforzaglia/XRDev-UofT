using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public Rigidbody cannonballPrefab;
    public Transform spawnPoint;
    public float shootingForce = 100f;
    public float turnSpeed = 40f;
    
    void Start()
    {
        InvokeRepeating("ShootAtPlayer", 3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!RobotPlayer())
        {
            return;
        }
        else
        {
            Vector3 targetDirection = RobotPlayer().transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void ShootAtPlayer()
    {
        if (RobotPlayer())
        {
            Rigidbody cannonBall = Instantiate(cannonballPrefab, spawnPoint.position, spawnPoint.rotation);
            cannonBall.AddForce(cannonBall.transform.forward * shootingForce);
            Destroy(cannonBall, 2f);
        }
    }

    private GameObject RobotPlayer()
    {
        var robotPlayer = FindObjectOfType<RobotTouchController>();
        if (robotPlayer)
        {
            return robotPlayer.gameObject;
        }
        else
        {
            return default;
        }
    }
}
