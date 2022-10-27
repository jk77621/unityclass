using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject stage;
    public GameObject stage2;
    int nBamCount = 0;
    int nCubeCount = 0;

    public void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.name)
        {
            case "bamsongi(Clone)":
                nBamCount++;
                break;
            case "Cube(Clone)":
                nCubeCount++;
                break;
        }

        if (nBamCount > 2 && nCubeCount > 1)
        {
            stage.GetComponent<ParticleSystem>().Play();
            stage2.GetComponent<ParticleSystem>().Play();
        }
    }
}
