using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float damageAmount;
    public float fireRate = 20f;
    public float force = 80;
    public int magazine = 30, amo, mags = 3;
    [HideInInspector] public bool isGrounded = false;

    public GameObject fireSpot;
    public ParticleSystem flash;
    public GameObject flashEffect;
    public GameObject bulletEffect;
    public GameObject bloodEffect;
    private Vector3 flashEffectPosition;
    public GameObject cameraObject;
    private Vector3 cameraPosition;
    public GameObject zoomSpot;

    public AudioClip fireAudioClip;
    public AudioClip reloadAudioClip;
    public AudioClip walkingAudioClip;
    public AudioClip landAudioClip;

    public AudioSource fireAudioSource;
    public AudioSource reloadAudioSource;
    public AudioSource walkAudioSource;
    public AudioSource landAudioSource;

    private Animator animations;
    private InputManager inputManager;
    private TakeDamage takeDamage;
    private GameObject shooter;

    public float reloadAnimationTime = 2.5f;
    private float reloadTime = 0.01f;
    private float readyToFire;
    private bool isReloading = false;
    public float lerpSpeed = 0.0001f;
    private bool isZooming = false;

    private int magazineTamp;

    private void Start()
    {
        //uiManager = GameObject.FindGameObjectWithTag("UISystem").GetComponent<UiManager>();
        animations = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        shooter = GetComponentInParent<PlayerController>().transform.gameObject;
        animations.SetInteger("Movement", 0);
        amo = magazine * mags;
        magazineTamp = magazine;
        flashEffectPosition = flashEffect.transform.localPosition;
        cameraPosition = cameraObject.transform.localPosition;

        //uiManager.setAmo(magazine + "/" + amo);

        fireAudioSource = spawnAudioSource(fireAudioClip);
        reloadAudioSource = spawnAudioSource(reloadAudioClip);
        walkAudioSource = spawnAudioSource(walkingAudioClip);
        landAudioSource = spawnAudioSource(landAudioClip);
    }

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            animations.SetBool("Sight", true);
            flashEffect.transform.localPosition = new Vector3(0f, flashEffectPosition.y, flashEffectPosition.z);
        }
        else
        {
            animations.SetBool("Sight", false);
            flashEffect.transform.localPosition = new Vector3(flashEffectPosition.x, flashEffectPosition.y, flashEffectPosition.z);
        }

        if (isZooming)
        {
            cameraObject.transform.localPosition = Vector3.Lerp(zoomSpot.transform.localPosition, cameraPosition, Time.deltaTime * lerpSpeed);
        }
        else
        {
            cameraObject.transform.localPosition = Vector3.Lerp(cameraPosition, zoomSpot.transform.localPosition, Time.deltaTime * lerpSpeed);
        }

        if (Time.time >= readyToFire)
        {
            animations.SetInteger("Fire", -1);
            animations.SetInteger("Movement", ((inputManager.vertical != 0 && isGrounded) || (inputManager.horizontal != 0 && isGrounded)) ? 1 : 0);
        }

        if ((Input.GetKeyDown(KeyCode.R) && !isReloading && amo > 0) || (magazine <= 0 && !isReloading && amo > 0))
        {
            reloadTime = reloadAnimationTime;
            animations.SetInteger("Reload", 1);
            isReloading = true;
        }

        if (isReloading && reloadTime <= 1)
        {
            reloadTime = 0;
            animations.SetInteger("Reload", -1);
            isReloading = false;
            amo = amo - magazineTamp + magazine;
            magazine = magazineTamp;
            if (amo < 0)
            {
                magazine += amo;
                amo = 0;
                //uiManager.setAmo(magazine + "/" + amo);
            }
            //uiManager.setAmo(magazine + "/" + amo);
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }

        Debug.DrawRay(fireSpot.transform.position, fireSpot.transform.forward, Color.blue);
    }

    public void zoomIn()
    {
        isZooming = true;
    }

    public void zoomOut()
    {
        isZooming = false;
    }

    public void fire()
    {
        if (Time.time >= readyToFire && !isReloading && magazine > 0)
        {
            readyToFire = Time.time + 1f / fireRate;
            animations.SetInteger("Fire", 2);
            animations.SetInteger("Movement", -1);
            takeDamage = null;

            flash.Play();
            RaycastHit hit;
            magazine--;
            //ui.setAmo(magazine + "/" +  amo);

            if (Physics.Raycast(fireSpot.transform.position, fireSpot.transform.forward, out hit))
            {
                Debug.DrawRay(fireSpot.transform.position, fireSpot.transform.forward, Color.red);
                checkHIT(hit);
            }
        }
    }

    private void checkHIT(RaycastHit hit)
    {
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * force);

        try
        {
            takeDamage = hit.transform.GetComponent<TakeDamage>();

            if (takeDamage != null)
            {
                Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            switch (takeDamage.damageType)
            {
                case TakeDamage.collisionType.head:
                    takeDamage.HIT(damageAmount, shooter);
                    break;
                case TakeDamage.collisionType.body:
                    takeDamage.HIT(damageAmount / 2, shooter);
                    break;
                case TakeDamage.collisionType.arms:
                    takeDamage.HIT(damageAmount / 4, shooter);
                    break;
            }


        }
        catch
        {
        }
    }

    public AudioSource spawnAudioSource(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = false;
        source.playOnAwake = false;
        source.volume = 1f;

        return source;
    }

    public void fireSound()
    {
        if (fireAudioSource.isPlaying)
            fireAudioSource.Stop();
        fireAudioSource.Play();
    }

    public void walkSound()
    {
        if (walkAudioSource.isPlaying)
            walkAudioSource.Stop();
        walkAudioSource.Play();
    }

    public void reloadSound()
    {
        if (reloadAudioSource.isPlaying)
            reloadAudioSource.Stop();
        reloadAudioSource.Play();
    }

    public void landSound()
    {
        if (landAudioSource.isPlaying)
            landAudioSource.Stop();
        landAudioSource.Play();
    }

}
