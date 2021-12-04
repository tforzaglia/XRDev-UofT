using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private AudioSource explosionSource;
    public GameObject explosionPrefab;
    public GameObject asteroidExplosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.collider.gameObject.tag == "Asteroid")
        {
            Instantiate(asteroidExplosionPrefab, transform.position, transform.rotation);
            Destroy(collision.gameObject);
        } else
        {
            var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 1f);
        }
    }
}