using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    [SerializeField]
    private GameObject stealth_vfx;
    public float delay = 7.5f;
    private float duration = 5.25f, cooldown = 0;
    private bool isStealth = false;
    
    void Update()
    {
        gameObject.GetComponent<Light>().enabled = !(cooldown == 0);
        if (!isStealth && cooldown == 0 && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(PlayerStealth());
        }
        cooldown -= Time.deltaTime;
        cooldown = Mathf.Clamp(cooldown, 0, delay);
    }

    IEnumerator PlayerStealth()
    {
        isStealth = true;
        GameObject vfx = Instantiate(stealth_vfx, transform);
        yield return new WaitForSeconds(duration);
        Destroy(vfx);
        isStealth = false;
        cooldown = 5;
    }

    public bool IsStealthed()
    {
        return isStealth;
    }
}
