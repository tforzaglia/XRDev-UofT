using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyParts : MonoBehaviour
{
    public Image brainIcon;
    public Image bladderIcon;
    public Image heartIcon;
    public Image leftLungIcon;
    public Image rightLungIcon;
    public Image stomachIcon;
    public Image leftKidneyIcon;
    public Image rightKidneyIcon;

    public void OnBrainFound()
    {
        brainIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnBladderFound()
    {
        bladderIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnHeartFound()
    {
        heartIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnLeftLungFound()
    {
        leftLungIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnRightLungFound()
    {
        rightLungIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnStomachFound()
    {
        stomachIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnLeftKidneyFound()
    {
        leftKidneyIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnRightKidneyFound()
    {
        rightKidneyIcon.color = new Color(1, 1, 1, 1);
    }
}
