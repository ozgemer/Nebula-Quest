using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTimeInitializer : MonoBehaviour
{
    public float time_min, time_max;
    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            float spawmTimer = Random.Range(time_min, time_max);
            transform.GetChild(i).gameObject.GetComponent<Spawner>().spawnDelay = spawmTimer;
        }
    }
}
