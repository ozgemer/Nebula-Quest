using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    //[SerializeField]
    //public Camera cam;
    [SerializeField]
    public Transform gunPosition;
    [SerializeField]
    private TextMeshProUGUI ammo;
    //[SerializeField]
    private PlayerCamera camScriptRef;

    public float shootDelay, reloadTime;
    public int magazineSize;
    private int bulletsLeft;
    bool shooting, readyToShoot, isReloading, allowInvoke;
    private AudioSource reloadAudio;


    private void Awake()
    {
        camScriptRef = GetComponent<PlayerCamera>();
        reloadAudio = new GameObject("ReloadObject").AddComponent<AudioSource>();
        reloadAudio.clip = Resources.Load("reload_sfx") as AudioClip;
        reloadAudio.volume = 0.2f;
        bulletsLeft = magazineSize;
        readyToShoot = true;
        allowInvoke = true;
    }

    void Update()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            Reload();
        if (readyToShoot && !isReloading && bulletsLeft <= 0)
            Reload();     

        if (ammo != null)
            ammo.SetText(bulletsLeft + "/" + magazineSize);

        if (camScriptRef.PlayerADS())
        {
            Vector3 rayCast = new Vector3(0.5f, 0.5f, 0);
            Ray ray = camScriptRef.CurrentCamera().ViewportPointToRay(rayCast);
            RaycastHit hit;
            Vector3 target;   
            if (Physics.Raycast(ray, out hit))
                target = hit.point;
            else            
                target = ray.GetPoint(100);

            Vector3 direction = target - gunPosition.position;
            Debug.DrawRay(gunPosition.position, direction.normalized * 100, Color.green);
            if(readyToShoot && shooting && !isReloading && bulletsLeft > 0)
                Shoot(target);
        }
    }

    private void Shoot(Vector3 target)
    {
        readyToShoot = false;
        GameObject currentBullet = Instantiate(bullet, gunPosition.position, Quaternion.identity);
        BulletController bulletController = currentBullet.GetComponent<BulletController>();
        bulletController.target = target;
        bulletController.hit = true;
        bulletsLeft--;

        if (allowInvoke)
        {
            Invoke("ResetShoot", shootDelay);
            allowInvoke = false;
        }
    }
    // time between shots
    private void ResetShoot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloadAudio.Play();
        isReloading = true;
        Invoke("Reloading", reloadTime);
    }
    // time of reloading
    private void Reloading()
    {
        reloadAudio.Play();
        bulletsLeft = magazineSize;
        isReloading = false;
    }
}
