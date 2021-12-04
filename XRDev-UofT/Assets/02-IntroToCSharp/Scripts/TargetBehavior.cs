using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
	public AudioClip explosionSound;
	public GameObject asteroidPrefab; 

	// when collided with another gameObject
	void OnCollisionEnter(Collision newCollision)
	{
		if (newCollision.gameObject.tag == "Laser")
		{
			AudioSource.PlayClipAtPoint(explosionSound, gameObject.transform.position);

			// destroy the laser object
			Destroy(newCollision.gameObject);

			Vector3 firstNew = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);
			Vector3 secondNew = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z);
			Vector3 scaleChange = new Vector3(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z * 0.5f);

			GameObject asteroidOne = Instantiate(asteroidPrefab, firstNew, transform.rotation);
			asteroidOne.GetComponent<TargetBehavior>().asteroidPrefab = asteroidPrefab;
			asteroidOne.transform.localScale = scaleChange;
			asteroidOne.GetComponent<Rigidbody>().AddForce(transform.right * -5000 * Time.deltaTime);
			asteroidOne.GetComponent<Rigidbody>().AddForce(transform.up * -5000 * Time.deltaTime);

			GameObject asteroidTwo = Instantiate(asteroidPrefab, secondNew, transform.rotation);
			asteroidTwo.GetComponent<TargetBehavior>().asteroidPrefab = asteroidPrefab;
			asteroidTwo.transform.localScale = scaleChange;
			asteroidTwo.GetComponent<Rigidbody>().AddForce(transform.right * +5000 * Time.deltaTime);
			asteroidTwo.GetComponent<Rigidbody>().AddForce(transform.up * +5000 * Time.deltaTime);

			Destroy(gameObject);
		}
	}
}
