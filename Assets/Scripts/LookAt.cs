using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private GameObject objToLookAt;

    void Update()
    {
        Look(objToLookAt.transform.position);
    }

    public void Look(Vector3 target)
    {
        Vector3 lookAt = target - transform.position;
        lookAt.y = 0;
        transform.rotation = Quaternion.LookRotation(lookAt);
        transform.Rotate(0, 85, 0);
    }
}
