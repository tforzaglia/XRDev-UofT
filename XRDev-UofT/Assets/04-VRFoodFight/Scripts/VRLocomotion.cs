using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRLocomotion : MonoBehaviour
{
    public Transform xrRig;
    public float playerSpeed = 1;
    public float curveHeight = 2;
    public int curveSegments = 20;

    public float fadeDuration = 1;
    public RawImage blackScreen;

    private VRInput vrInput;
    private LineRenderer lineRenderer;

    void Start()
    {
        vrInput = GetComponent<VRInput>();

        lineRenderer = GetComponent<LineRenderer>();
        // start disabled
        lineRenderer.enabled = false;
        lineRenderer.positionCount = curveSegments;
    }

    void Update()
    {
        HandleRayCast();
        HandleRotation();
        HandleMovement();
    }

    void HandleRayCast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            lineRenderer.enabled = true;

            // draw a curved line
            CalculateCurve(transform.position, hitInfo.point);

            // draw a straight line
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, hitInfo.point);

            bool validTarget = hitInfo.collider.CompareTag("Teleportation");
            Color color = validTarget ? Color.blue : Color.red;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;

            if (validTarget && Input.GetButtonDown($"XRI_{vrInput.hand}_TriggerButton"))
            {
                StartCoroutine(FadeAndTeleport(hitInfo.point));
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void HandleRotation()
    {
        // detect if the thumbstick is pressed
        if (Input.GetButtonDown($"XRI_{vrInput.hand}_Primary2DAxisClick"))
        {
            // detect the direction
            float rotation = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Horizontal") > 0 ? 30 : -30;

            // apply the rotation to the XRrig
            xrRig.Rotate(0, rotation, 0);
        }
    }

    void HandleMovement()
    {
        Vector3 forwardDirection = new Vector3(xrRig.forward.x, 0, xrRig.forward.z);
        Vector3 rightDirection = new Vector3(xrRig.right.x, 0, xrRig.right.z);

        forwardDirection.Normalize();
        rightDirection.Normalize();

        // how hard is the user pressing on the thumbstick
        // values go from -1 to 1
        float horizontalValue = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Horizontal");
        float verticalValue = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Vertical");

        // forward and backward
        xrRig.position = xrRig.position + (verticalValue * playerSpeed * -forwardDirection * Time.deltaTime);

        // left and right
        xrRig.position = xrRig.position + (horizontalValue * playerSpeed * rightDirection * Time.deltaTime);
    }

    void CalculateCurve(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 midPoint = (startPoint + endPoint) / 2;
        Vector3 controlPoint = midPoint + Vector3.up * curveHeight;

        for (int i = 0; i < curveSegments; i++)
        {
            float percent = i / (float) curveSegments;
            Vector3 a = Vector3.Lerp(startPoint, controlPoint, percent);
            Vector3 b = Vector3.Lerp(controlPoint, endPoint, percent);

            Vector3 curvePoint = Vector3.Lerp(a, b, percent);

            lineRenderer.SetPosition(i, curvePoint);
        }
        lineRenderer.SetPosition(curveSegments - 1, endPoint);
    }

    private IEnumerator FadeAndTeleport(Vector3 teleportationPoint)
    {
        float currentTime = 0;

        // fade to black
        while (currentTime < fadeDuration)
        {
            blackScreen.color = Color.Lerp(Color.clear, Color.black, currentTime);
            yield return new WaitForEndOfFrame();
            currentTime = currentTime + Time.deltaTime;
        }
        blackScreen.color = Color.black;

        // teleport
        xrRig.position = teleportationPoint;

        yield return new WaitForSeconds(1);

        // fade from black
        currentTime = 0;
        while (currentTime < fadeDuration)
        {
            blackScreen.color = Color.Lerp(Color.black, Color.clear, currentTime);
            yield return new WaitForEndOfFrame();
            currentTime = currentTime + Time.deltaTime;
        }

        blackScreen.color = Color.clear;
    }
}
