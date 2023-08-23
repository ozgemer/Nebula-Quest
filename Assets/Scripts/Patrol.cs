using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    private GameObject vfx;
    private GameObject currentVFX;

    public Transform[] waypoints;
    public float speed = 10f;
    public float idleTime = 5f;
    private bool isIdle = false;
    private bool isDisabled = false;
    private Vector3 target;
    private int currentWaypoint = 0;
    private float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        target = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isIdle && !isDisabled) {
            if (Vector3.Distance(transform.position, target) < 0.2f)
                target = waypoints[++currentWaypoint % waypoints.Length].position;

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            LookAt(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((!isIdle || isDisabled) && collision.gameObject.tag == "Bullet")
        {
            currentVFX = Instantiate(vfx, transform.position, Quaternion.identity);
            StartCoroutine(PatrolIdle());
        }
    }

    IEnumerator PatrolIdle()
    {
        isIdle = true;
        gameObject.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(idleTime);
        gameObject.GetComponent<Light>().enabled = false;
        isIdle = false;
        Destroy(currentVFX);
    }

    public void LookAt(Vector3 target)
    {
        Vector3 lookAt = target - transform.position;
        lookAt.y = 0;
        if(lookAt != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), Time.deltaTime * rotationSpeed);
    }

    public void Stop()
    {
        isDisabled = true;
    }

    public void Continue()
    {
        isDisabled = false;
    }

    public bool IsPatrolIdle()
    {
        return isIdle;
    }
}
