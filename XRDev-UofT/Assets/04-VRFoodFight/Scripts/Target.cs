using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float moveSpeed;
    public float moveAmount;
    public AudioClip hitSound;
    public SpawnArea game;

    private AudioSource audioSource;
    private float startingXPosition;

    void Start()
    {
        startingXPosition = transform.position.x;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = startingXPosition + Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var food = collision.gameObject.GetComponent<GrabbableObject>();
        if (food != null)
        {
            audioSource.PlayOneShot(hitSound);
            Destroy(food.gameObject);

            game.OnTargetHit();

            Destroy(gameObject);
        }
    }
}
