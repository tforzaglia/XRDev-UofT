using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Interactable
{
    [SerializeField] private Light light;

    protected override void Interact()
    {
        light.enabled = !light.enabled;
    }
}
