using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityStandardAssets.CrossPlatformInput;

public class Shooting : MonoBehaviour
{

    private Camera mainCam;
    private CinemachineVirtualCamera cCam;
    private Vector3 mousePos;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private float shakeIntensity;


    public GameObject bullet;
    public Transform bulletTransform;
    private bool canFire;
    private float timer;


    public AudioSource shootingSound;

    private PlayerScript player;
    private WeaponScript weaponScript;


    public int gunAmmo;
    public int shotGunAmmo;
    public int ak47Ammo;
    public int bazookaAmmo;


    public ShootJoystick joystick;

    public Vector3 rotation;
    private Vector3 localScale;


    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //cCam = GameObject.FindGameObjectWithTag("CCamera").GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        shootingSound = GetComponent<AudioSource>();
        weaponScript = GetComponent<WeaponScript>();
        canFire = true;
        shakeIntensity = 2;


    }

    private void FixedUpdate()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //rotZ = Mathf.Atan2(joystick.vertical, joystick.horizontal) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotZ);


        localScale = Vector3.one;

        if (rotZ > 90 || rotZ <-90f)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        transform.localScale = localScale;


        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > weaponScript.timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && canFire && !player.won)
        {
            if (weaponScript.currentWeapon == 1 && gunAmmo > 0)
            {
                gunAmmo--;
                player.isHidden = false;
                canFire = false;
                shootingSound.pitch = Random.Range(.9f, 1.1f);
                shootingSound.Play();
                //ShakeCamera(shakeIntensity, .1f);
                Shoot();

            }
            if (weaponScript.currentWeapon == 2 && shotGunAmmo > 0)
            {
                shotGunAmmo--;
                player.isHidden = false;
                canFire = false;
                shootingSound.Play();
                //ShakeCamera(shakeIntensity, .1f);
                Shoot();
            }
            if (weaponScript.currentWeapon == 3 && ak47Ammo > 0)
            {
                ak47Ammo--;
                player.isHidden = false;
                canFire = false;
                shootingSound.Play();
                //ShakeCamera(shakeIntensity, .1f);
                Shoot();
            }
            if (weaponScript.currentWeapon == 4 && bazookaAmmo > 0)
            {
                bazookaAmmo--;
                player.isHidden = false;
                canFire = false;
                shootingSound.Play();
                //ShakeCamera(shakeIntensity, .1f);
                Shoot();
            }




        }
              

    }

    private void Shoot()
    {
        Instantiate(bullet, bulletTransform.position, Quaternion.Euler(0,0,0));

        /*
        GameObject bullet = BulletPool.instance.GetBullets();

        if(bullet != null)
        {
            bullet.transform.position = bulletTransform.position;
            bullet.SetActive(true);
            bullet.GetComponent<BulletScript>().Restart();
        }
        */
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

}
