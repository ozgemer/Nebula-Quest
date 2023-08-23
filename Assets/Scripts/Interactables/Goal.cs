using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : Interactable
{
    [SerializeField]
    private GameObject win_vfx;
    [SerializeField]
    private GameObject fail_vfx;
    private GameObject current_vfx;
    private AudioSource sfx;
    private GameObject player;

    private void Awake()
    {
        sfx = gameObject.GetComponent<AudioSource>();
    }

    protected override void Interact()
    {
        player = GameObject.Find("Player");
        if (current_vfx != null)
            Destroy(current_vfx);
        if (player.GetComponent<FuelBar>().isFull())
        {
            sfx.Play();
            current_vfx = Instantiate(win_vfx, player.transform);
            StartCoroutine(NextLevel());
        }
        else        
            current_vfx = Instantiate(fail_vfx, player.transform);
        
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
