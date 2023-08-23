using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawn;
    public float spawnDelay = 1.5f;
    private GameObject current;
    private bool isSpawning = false;

    private void Awake()
    {
        current = Instantiate(spawn, transform);
    }

    public void Update()
    {
        if (!isSpawning && !current)
            StartCoroutine("Respawn");                
    }

    IEnumerator Respawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);
        current = Instantiate(spawn, transform);
        isSpawning = false;
    }
}
