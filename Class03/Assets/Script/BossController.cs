using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject arrow;
    float delta = 0f;
    float span = 3.0f;

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;

            int rndNum = Random.Range(0, 2);

            GameObject genObj = Instantiate(arrow);
            genObj.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            genObj.transform.rotation = Quaternion.Euler(0, 0, rndNum == 1 ? 90 : 270);

            transform.localScale = new Vector3(rndNum == 1 ? 5 : -5, 5, 1);
        }
    }
}
