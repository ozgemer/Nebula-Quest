using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject hit_vfx;
    private GameObject current_hit;
    [SerializeField]
    private GameObject death_vfx;
    private HealthBar health;

    private void Awake()
    {
        health = gameObject.GetComponent<HealthBar>();
    }

    public void TakeDamage()
    {
        health.TakeDamage();
        gameObject.GetComponent<Animator>().SetTrigger("TakeDamage");
        if (health.getCurrentHealth() == 0)
            Die();
        else
        {
            if (current_hit != null)
                Destroy(current_hit);
            current_hit = Instantiate(hit_vfx, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    public void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        Instantiate(death_vfx, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Lose Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
            TakeDamage();
    }
}
