using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityScript : MonoBehaviour
{
    Vector2 mypos = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        gameObject.transform.position = mypos;
        mypos.x += 0.01f;
        gameObject.transform.position = mypos;

    }
}
