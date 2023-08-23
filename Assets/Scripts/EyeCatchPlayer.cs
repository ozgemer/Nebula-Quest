using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCatchPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject radius_vfx, kill_vfx;
    [SerializeField]
    private Transform eyeBase;
    [SerializeField]
    private GameObject eye;
    [SerializeField]
    private Animator anim;
    public float distanceToKill = 3f, timeToKill = 3f;

    private GameObject current_radius, target;
    private bool isCharge = false;

    private void Update()
    {
        if (target)
            eye.transform.LookAt(target.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
            isCharge = false;
            anim.SetBool("Attack", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!eye.GetComponent<Eye>().IsDisabled() && !isCharge && other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            isCharge = true;
            StartCoroutine(KillPlayer(other.gameObject));
            anim.SetBool("Attack", true);
        }
    }

    IEnumerator KillPlayer(GameObject player)
    {
        if (current_radius)
            Destroy(current_radius);
        current_radius = Instantiate(radius_vfx, eyeBase);
        yield return new WaitForSeconds(timeToKill);
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToKill)
        {
            Instantiate(kill_vfx, transform);
            player.GetComponent<PlayerDamage>().Die();
        }
        Destroy(current_radius);
        isCharge = false;
    }
}
