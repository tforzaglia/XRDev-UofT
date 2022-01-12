using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    // what we are touching
    public GrabbableObject collidingObject;

    // what we are holding
    public GrabbableObject heldObject;

    public VRInput controller;

    public float throwForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab)
        {
            collidingObject = grab;
            collidingObject.OnHoverStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab == collidingObject)
        {
            collidingObject.OnHoverEnd();
            collidingObject = null;
        }
    }

    public void Grab()
    {
        if (collidingObject != null)
        {
            heldObject = collidingObject;
            heldObject.ParentGrab(controller);
        }
    }
    public void Release()
    {
        if (heldObject)
        {
            heldObject.ParentRelease();

            // throw
            heldObject.grabbableRigidBody.velocity = controller.velocity * throwForce;
            heldObject.grabbableRigidBody.angularVelocity = controller.angularVelocity * throwForce;

            heldObject = null;
        }
    }

    void Start()
    {
        controller = GetComponent<VRInput>();
        controller.OnGripDown.AddListener(Grab);
        controller.OnGripUp.AddListener(Release);
    }

    void Update()
    {
        
    }
}
