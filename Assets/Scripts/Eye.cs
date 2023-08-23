using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private Animator anim;
    public float idleTime = 12.5f;
    private bool isIdle = false, disabled = false;
    private FollowPlayer follow;

    void Awake()
    {
        gameObject.TryGetComponent<FollowPlayer>(out follow);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isIdle && follow)
            follow.Follow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isIdle && collision.gameObject.tag == "Bullet")
            StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        isIdle = true;
        if(follow)
            follow.Stop();
        anim.SetTrigger("Disable");
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
        if (follow)
            follow.Continue();
        anim.SetTrigger("Activate");
    }

    public void Disable()
    {
        disabled = true;
    }

    public void Activate()
    {
        disabled = false;
    }

    public bool IsDisabled()
    {
        return disabled;
    }
}
