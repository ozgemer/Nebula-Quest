using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatRotate : MonoBehaviour
{
    public float floatSpeed = 2.5f;
    public float floatHeight = 5f;
    public float rotateSpeed = 50f;
    private float originalY;
    private float SIN_MULTI = 0.5f;
    private float HEIGHT_MULTI = 0.25f;

    private void Awake()
    {
        originalY = transform.position.y;
    }
    void Update()
    {
        float newY = Mathf.Abs(Mathf.Sin(Time.time * floatSpeed) / SIN_MULTI * floatHeight * HEIGHT_MULTI);
        transform.position = new Vector3(transform.position.x, originalY + newY, transform.position.z);
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
