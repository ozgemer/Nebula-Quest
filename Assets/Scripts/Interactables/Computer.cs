using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField]
    private GameObject vfx;
    [SerializeField]
    private Canvas[] worldUI;
    private GameObject current_vfx;
    private AudioSource audio;
    private AudioSource sfx;

    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
        current_vfx = Instantiate(vfx, gameObject.transform);
        sfx = new GameObject("uiSFX").AddComponent<AudioSource>();
        sfx.clip = Resources.Load("ui_sfx") as AudioClip;
        sfx.volume = 0.2f;
    }

    protected override void Interact()
    {
        sfx.Play();
        foreach (Canvas ui in worldUI)
            ui.enabled = true;
        if(current_vfx)
            Destroy(current_vfx);
        audio.enabled = false;
    }
}
