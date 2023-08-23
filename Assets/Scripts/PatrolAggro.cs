using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAggro : MonoBehaviour
{
    [SerializeField]
    private GameObject passiveR, activeR, lazer;
    [SerializeField]
    private Patrol patrol;

    private bool isActive = false, isCharging = false;
    private GameObject target;

    private void Awake()
    {
        activeR.SetActive(false);
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!target.GetComponent<Stealth>().IsStealthed())
        {
            if (isActive && !patrol.IsPatrolIdle())
            {
                patrol.LookAt(target.transform.position);
                if (!isCharging)
                    StartCoroutine(ShootLazer());
            }
        }
        else
        {
            Passive();
            patrol.Continue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") {
            patrol.Stop();
            Active();
        }
    }

    IEnumerator ShootLazer()
    {
        isCharging = true;
        yield return new WaitForSeconds(1.5f);
        if (isActive && !patrol.IsPatrolIdle())
        {
            GameObject currentBullet = Instantiate(lazer, transform.position, Quaternion.identity);
            BulletController bulletController = currentBullet.GetComponent<BulletController>();
            bulletController.target = GameObject.Find("Player").transform.position;
            bulletController.hit = true;
        }
        isCharging = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Passive();
            patrol.Continue();
        }
    }

    private void Active()
    {
        passiveR.SetActive(false);
        activeR.SetActive(true);
        isActive = true;
    }

    private void Passive()
    {
        passiveR.SetActive(true);
        activeR.SetActive(false);
        isActive = false;
    }
}
