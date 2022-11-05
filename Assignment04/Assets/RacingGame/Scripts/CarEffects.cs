using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    public ParticleSystem[] smoke;
    private PlayerController controller;
    private bool smokeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
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
}
