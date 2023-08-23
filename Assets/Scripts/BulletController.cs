using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject vfx;
    public float speed = 150f;
    public float DESTROY_DELAY = 3.25f;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    private bool waitingToDestroy = false;
    public bool isPhyisical = true;

    void Awake()
    {
        Destroy(gameObject, DESTROY_DELAY);
    }
    private void Start()
    {
        if(isPhyisical)
            Shoot();
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 lookAt = target - transform.position;
            if (lookAt != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookAt);
            transform.Rotate(new Vector3(90, 0, 0));
        }
        else
        {
            if(!waitingToDestroy)
                DestroyBullet(); 
        }
    }

    private void Shoot() 
    {
        Vector3 direction = target - transform.position;
        transform.forward = direction.normalized;
        gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 2 && isPhyisical)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            DestroyBullet();
        }     
    }

    private void DestroyBullet()
    {
        waitingToDestroy = true;
        Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.25f);
    }
}
