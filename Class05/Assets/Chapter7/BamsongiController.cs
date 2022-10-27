using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiController : MonoBehaviour
{
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "target")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<ParticleSystem>().Play();
        }
    }

    void Start()
    {
        //Shoot(new Vector3(0, 200, 2000));
    }
}
