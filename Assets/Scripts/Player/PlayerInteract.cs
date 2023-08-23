using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private GameObject rayOrigin;
    [SerializeField]
    private float interactDistance = 3f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject prompt;
    [SerializeField]
    private HUDInteract playerUI;
    private bool isInteract = false;

    void Update()
    {
        Ray ray = new Ray(rayOrigin.transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction* interactDistance, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, mask))
        {
            Interactable interactable;
            if (interactable = hit.collider.GetComponent<Interactable>())
            {            
                if(!isInteract)
                    playerUI.UpdateText(interactable.promptMessage);

                if (Input.GetKeyDown(KeyCode.E))
                    interactable.BaseInteract();
            }            
            prompt.SetActive(isInteract = true);
        }
        else        
            prompt.SetActive(isInteract = false);
    }
}
