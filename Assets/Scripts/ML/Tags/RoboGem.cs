using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGem : MonoBehaviour
{
    [SerializeField]
    private GameObject env;

    public void DestroyML()
    {
        Destroy(env, 0.25f);
    }
}
