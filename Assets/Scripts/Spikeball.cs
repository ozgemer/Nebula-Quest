using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeball : MonoBehaviour
{
    [SerializeField]
    private GameObject vfx;
    public float explosionForce, explosionRadius;
    private AudioSource sfx;
    private GameObject player;
    private Transform playerOriginal;

    private void Awake()
    {
        sfx = gameObject.GetComponent<AudioSource>();
    }

    private void Knockback()
    {
        playerOriginal = player.transform;
        player.GetComponent<CharacterController>().enabled = false;
        Rigidbody playerrb = player.GetComponent<Rigidbody>();
        playerrb.AddForce(Vector3.up * 7.5f, ForceMode.Impulse);
        playerrb.constraints = RigidbodyConstraints.None;
        playerrb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        StartCoroutine(ResetPlayer());
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(2.5f);
        Rigidbody playerrb = player.GetComponent<Rigidbody>();
        playerrb.constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<CharacterController>().enabled = true;
        player.transform.rotation = Quaternion.Euler(Vector3.zero);
        player.transform.forward = transform.position - player.transform.position;
        player.GetComponent<PlayerDamage>().TakeDamage();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        sfx.Play();
        GameObject expVFX = Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(expVFX, 1.5f);
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            Knockback();
        }
        else        
            Destroy(gameObject, 0.2f);
        
    }   
}
