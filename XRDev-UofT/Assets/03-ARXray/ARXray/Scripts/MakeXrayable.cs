using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeXrayable : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().material.renderQueue = 3001;
        }
    }
}
