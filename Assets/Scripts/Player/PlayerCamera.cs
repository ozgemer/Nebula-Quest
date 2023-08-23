using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private GameObject firstPerson, thirdPerson;
    private Camera currentCam;

    private float distance = 9;
    private float offset = 20;
    private Vector3 zoomTransform;
    private Animator anim;
    private Transform camTransform;
    private float currentX = 0;
    private float currentY = 0;
    private bool isScope = false;
    private bool is1stPerson = false;
    private int MAX_ANGLE = 55;
    private int MIN_ANGLE = 0;

    private void Start()
    {
        anim = transform.GetComponent<Animator>();
        currentCam = thirdPerson.GetComponent<Camera>();
        camTransform = currentCam.transform;
        zoomTransform = new Vector3(2, -0.5f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            changePerspective();

        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");
        currentX = currentX % 360;
        currentY = currentY % 360;

        if (!is1stPerson) // 3rd person
        {
            currentY = Mathf.Clamp(currentY, MIN_ANGLE, MAX_ANGLE);
            Vector3 dir = new Vector3(0, 0, -distance);
            if (isScope)
                transform.rotation = Quaternion.Euler(0, currentX, 0);

            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = transform.position + rotation * dir;
            camTransform.LookAt(transform.position);
            camTransform.Rotate(-offset, 0, 0);
        }
        else //1st person
        {
            if (currentY < 75 && currentY > -75)
                camTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));
            else
                currentY = Mathf.Clamp(currentY, -75, 75);

            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        }


        if (Input.GetMouseButtonDown(1))
        {
            currentCam.fieldOfView = 40;
            isScope = true;
            anim.SetTrigger("Aim");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            currentCam.fieldOfView = 60;
            isScope = false;
            anim.SetTrigger("Unaim");
        }
        if (isScope && !is1stPerson)
            currentCam.transform.position += transform.TransformDirection(zoomTransform);
    }

    private void changePerspective()
    {
        is1stPerson = !is1stPerson;
        if (is1stPerson)
        {
            thirdPerson.GetComponent<Camera>().enabled = false;
            firstPerson.GetComponent<Camera>().enabled = true;
            thirdPerson.SetActive(false);
            firstPerson.SetActive(true);
            currentCam = firstPerson.GetComponent<Camera>();
        }
        else
        {
            thirdPerson.GetComponent<Camera>().enabled = true;
            firstPerson.GetComponent<Camera>().enabled = false;
            thirdPerson.SetActive(true);
            firstPerson.SetActive(false);
            currentCam = thirdPerson.GetComponent<Camera>();
        }
        camTransform = currentCam.transform;
        currentCam.transform.rotation = transform.rotation;
    }

    public bool PlayerADS()
    {
        return isScope;
    }

    public bool IsFirstPerson()
    {
        return is1stPerson;
    }

    public Camera CurrentCamera()
    {
        return currentCam;
    }
}
