using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : GrabbableObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected virtual void Update()
    {
        if (controller != null)
        {
            if (Input.GetButtonDown($"XRI_{controller.hand}_TriggerButton"))
            {
                Interact();
            }
        }       
    }

    protected abstract void Interact();
}
