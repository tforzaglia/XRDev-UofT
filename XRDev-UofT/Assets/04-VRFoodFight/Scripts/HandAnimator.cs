using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    private VRInput controller;
    private Animator handAnimator;

    void Start()
    {
        controller = GetComponent<VRInput>();
        handAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (controller)
        {
            handAnimator.Play("Fist Closing", 0, controller.gripValue);
        }
    }
}
