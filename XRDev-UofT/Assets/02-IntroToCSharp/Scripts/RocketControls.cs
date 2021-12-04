using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControls : MonoBehaviour
{
    // force on the object
    public float thrust = 1000;
    public float dragForce;

    public Rigidbody laserPrefab;
    public Transform spawnPoint;
    public float laserImpulse;
    public Light engineLight;
    public AudioClip laserSound;

    private Rigidbody rocketRigidBody;
    private AudioSource audioSource;

    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // only allow rocket movement when pressing the right mouse button down
        if (Input.GetKey(KeyCode.Mouse1)) 
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            Vector3 horizontal = new Vector3(0, mouseX * thrust * Time.deltaTime, 0);
            Vector3 vertical = new Vector3(mouseY * thrust * Time.deltaTime, 0, 0);

            rocketRigidBody.AddRelativeTorque(horizontal);
            rocketRigidBody.AddRelativeTorque(vertical);
        }

        bool engineOn = false;

        // move forward
        if (Input.GetKey(KeyCode.W)) 
        {
            rocketRigidBody.AddForce(transform.forward * thrust * Time.deltaTime);
            engineOn = true;
        }
        // move backwards
        if (Input.GetKey(KeyCode.S)) {
            rocketRigidBody.AddForce(transform.forward * -thrust * Time.deltaTime);
            engineOn = true;
        }

        // move left
        if (Input.GetKey(KeyCode.A)) {
            rocketRigidBody.AddForce(transform.right * -thrust * Time.deltaTime);
            engineOn = true;
        }

        // move right
        if (Input.GetKey(KeyCode.D)) {
            rocketRigidBody.AddForce(transform.right * thrust * Time.deltaTime);
            engineOn = true;
        }

        // add drag force
        rocketRigidBody.AddForce(-rocketRigidBody.velocity * dragForce * Time.deltaTime);

        // fire a laser when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            FireLaser();
        }

        // enable the engine light when moving
        engineLight.enabled = engineOn;
    }

    private void FireLaser() 
    {
        Rigidbody laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);
        laser.velocity = rocketRigidBody.velocity;
        laser.AddForce(transform.forward * laserImpulse);
        audioSource.PlayOneShot(laserSound);
    }
}
