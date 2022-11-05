using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    public AudioSource skidClip;
    public TrailRenderer[] tireMarks;
    public ParticleSystem[] smoke;
    public ParticleSystem[] nitrusSmoke;
    private PlayerController controller;
    private InputManager IM;
    private bool smokeFlag = false, tireMarksFlag;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
        IM = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        chectDrift();
        activateSmoke();
    }

    private void activateSmoke()
    {
        if (controller.playPauseSmoke) startSmoke();
        else stopSmoke();

        if (smokeFlag)
        {
            for (int i = 0; i < smoke.Length; i++)
            {
                var emission = smoke[i].emission;
                emission.rateOverTime = ((int)controller.KPH * 10 <= 2000) ? (int)controller.KPH * 10 : 2000;
            }
        }
    }

    public void startSmoke()
    {
        if (smokeFlag) return;
        for (int i = 0; i < smoke.Length; i++)
        {
            var emission = smoke[i].emission;
            emission.rateOverTime = ((int)controller.KPH * 2 >= 2000) ? (int)controller.KPH * 2 : 2000;
            smoke[i].Play();
        }
        smokeFlag = true;

    }

    public void stopSmoke()
    {
        if (!smokeFlag) return;
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].Stop();
        }
        smokeFlag = false;
    }

    public void startNitrusEmitter()
    {
        if (controller.nitrusFlag) return;
        for (int i = 0; i < nitrusSmoke.Length; i++)
        {
            nitrusSmoke[i].Play();
        }
        controller.nitrusFlag = true;
    }
    public void stopNitrusEmitter()
    {
        if (!controller.nitrusFlag) return;
        for (int i = 0; i < nitrusSmoke.Length; i++)
        {
            nitrusSmoke[i].Stop();
        }
        controller.nitrusFlag = false;
    }

    private void chectDrift()
    {
        if (IM.handbrake) startEmitter();
        else stopEmitter();
    }

    private void startEmitter()
    {
        if (tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = true;
        }
        skidClip.Play();
        tireMarksFlag = true;
    }
    private void stopEmitter()
    {
        if (!tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = false;
        }
        skidClip.Stop();
        tireMarksFlag = false;
    }
}
