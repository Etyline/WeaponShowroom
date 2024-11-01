using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GunSystem : MonoBehaviour
{

    //Audio
    [SerializeField] WwiseWeapons WwiseSoundWeapons;

    //Gun Stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;


    //bools 
    bool shooting, readyToShoot, reloading;


    //Reference
    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;


    //Graphics
    public GameObject bulletHoleGraphic;
    public ParticleSystem muzzleFlash;
    public TextMeshProUGUI text;

    //Animation
    private Animator mAnimator;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();


        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            WwiseSoundWeapons.OnShoot();
        }
        if (shooting && bulletsLeft == 0) WwiseSoundWeapons.OnShoot();
        
    }

    private void MuzzleFlashPlay()
    {
        muzzleFlash.Play();
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //RayCast
        MuzzleFlashPlay();

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            if (((1 << rayHit.collider.gameObject.layer) & whatIsEnemy) != 0)
            {
                Debug.Log(rayHit.collider.name);

                if (rayHit.collider.CompareTag("Target"))
                {
                    rayHit.collider.GetComponent<Target>().TakeDmg(damage);
                }
                
            }
            if(rayHit.collider.CompareTag("Surface"))
            {
                Debug.Log("Hit");
                AK.Wwise.Switch targetSurface = rayHit.collider.gameObject.GetComponent<Surface>().surfaceType;
                Debug.Log(targetSurface.GroupId);
                GameObject instance = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                AkSoundEngine.SetSwitch(targetSurface.GroupId, targetSurface.Id, instance);
                instance.GetComponent<PlayImpact>().PlaySound();
            }
           

        }


        bulletsLeft--;
        bulletsShot--;


        Invoke("ResetShot", timeBetweenShooting);


        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        mAnimator.SetTrigger("Reload");
        WwiseSoundWeapons.OnReload();
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

