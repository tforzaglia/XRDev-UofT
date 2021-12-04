using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float speed = 20.0f;
    public Vector3 rotation = new Vector3(0, 1, 0);
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
       if(target == null)
        {
            target = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, rotation, Time.deltaTime * speed);
    }
}
