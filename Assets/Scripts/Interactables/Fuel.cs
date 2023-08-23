using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Interactable
{
    private GameObject player;
    private AudioSource collect_sfx;
    private void Awake()
    {
        collect_sfx = gameObject.GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    protected override void Interact()
    {
        player.GetComponent<FuelBar>().AddFuel();
        collect_sfx.Play();
        Destroy(gameObject, 0.25f);
    }
}
