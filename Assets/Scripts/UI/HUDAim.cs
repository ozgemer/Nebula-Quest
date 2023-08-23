using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDAim : MonoBehaviour
{
    [SerializeField]
    private Image crosshair;

    private void Awake()
    {
        crosshair.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))        
            crosshair.enabled = true;
        else if (Input.GetMouseButtonUp(1))        
            crosshair.enabled = false;
        
    }
}
