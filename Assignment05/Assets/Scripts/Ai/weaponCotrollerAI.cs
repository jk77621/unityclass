using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class weaponCotrollerAI : MonoBehaviour
{

    public float damageAmount;
    public float fireRate = 20f;
    public float force = 80;
    public int magazine = 30, amo, mags = 3;
    [HideInInspector] public bool isGrounded = false;

    public GameObject fireSpot;
    public ParticleSystem flash;
    public GameObject bulletEffect;
    public GameObject bloodEffect;

    public AudioClip fireAudioClip;
    public AudioClip reloadAudioClip;
    public AudioClip walkingAudioClip;
    public AudioClip landAudioClip;

    public AudioSource fireAudioSource;
    public AudioSource reloadAudioSource;
    public AudioSource walkAudioSource;
    public AudioSource landAudioSource;

    private Animator animations;
    private AiController aiController;
    private TakeDamage takeDamage;
    private GameObject shooter;

    public float reloadAnimationTime = 2.5f;
    private float reloadTime = 0;
    private float readyToFire;
    private bool isReloading = false;

    private int magazineTamp;

    private void Start()
    {
        //uiManager = GameObject.FindGameObjectWithTag("UISystem").GetComponent<UiManager>();
        animations = GetComponent<Animator>();
        aiController = GetComponentInParent<AiController>();
        shooter = GetComponentInParent<AiController>().transform.gameObject;
        animations.SetInteger("Movement", 0);
        amo = magazine * mags;
        magazineTamp = magazine;

        //uiManager.setAmo(magazine + "/" + amo);

        fireAudioSource = spawnAudioSource(fireAudioClip);
        reloadAudioSource = spawnAudioSource(reloadAudioClip);
        walkAudioSource = spawnAudioSource(walkingAudioClip);
        landAudioSource = spawnAudioSource(landAudioClip);
    }

    private void Update()
    {
        if (Time.time >= readyToFire)
        {
            animations.SetInteger("Fire", -1);
            animations.SetInteger("Movement", aiController.verticalInput != 0 && isGrounded ? 1 : 0);
        }

        if (magazine <= 0 && !isReloading && amo > 0)
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
                //ui.setAmo(magazine + "/" +  amo);
            }
            //ui.setAmo(magazine + "/" +  amo);
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }

        Debug.DrawRay(fireSpot.transform.position, fireSpot.transform.forward, Color.blue);
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
        source.volume = 0.1f;

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
