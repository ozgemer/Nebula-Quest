using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crystal : Interactable
{
    //private TextMeshProUGUI gemsCounter;
    private GameObject player;
    private AudioSource collect_sfx;
    private RoboGem isRobo;

    private void Awake()
    {
        collect_sfx = gameObject.GetComponent<AudioSource>();
        //gemsCounter = GameObject.Find("Gem Text").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
        gameObject.TryGetComponent<RoboGem>(out isRobo);
    }

    protected override void Interact()
    {
        collect_sfx.Play();
        Destroy(gameObject, 0.25f);
        player.GetComponent<GemsCounter>().Add();
        if (isRobo)
            isRobo.DestroyML();
    }
}
