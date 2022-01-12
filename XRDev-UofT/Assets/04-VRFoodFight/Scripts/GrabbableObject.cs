using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class GrabbableObject : MonoBehaviour
{
    public Rigidbody grabbableRigidBody;
    public Renderer grabbableRenderer;
    public Color hoverColor;
    public Color nonHoverColor;

    protected VRInput controller;

    protected virtual void Start()
    {
        grabbableRigidBody = GetComponent<Rigidbody>();
        grabbableRenderer = GetComponent<Renderer>();
    }

    public void OnHoverStart()
    {
        grabbableRenderer.material.color = hoverColor;
    }

    public void OnHoverEnd()
    {
        grabbableRenderer.material.color = nonHoverColor;
    }

    public void ParentGrab(VRInput controller)
    {
        this.controller = controller;
        transform.SetParent(controller.transform);
        grabbableRigidBody.useGravity = false;
        grabbableRigidBody.isKinematic = true;
    }

    public void ParentRelease()
    {
        this.controller = null;
        transform.SetParent(null);
        grabbableRigidBody.useGravity = true;
        grabbableRigidBody.isKinematic = false;
    }

    public void JointGrab()
    {

    }

    public void JointRelease()
    {

    }
}
