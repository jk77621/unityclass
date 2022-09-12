using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject arrowPrefab1;
    public GameObject arrowPrefab2;
    public GameObject arrowPrefab3;
    float span = 1.0f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;

            GameObject genObj = Instantiate(arrowPrefab);
            genObj.transform.position = new Vector3(0, 7, 0);
            
            GameObject genObj1 = Instantiate(arrowPrefab1);
            genObj1.transform.position = new Vector3(-10, 0, 0);

            GameObject genObj2 = Instantiate(arrowPrefab2);
            genObj2.transform.position = new Vector3(0, -7, 0);

            GameObject genObj3 = Instantiate(arrowPrefab3);
            genObj3.transform.position = new Vector3(10, 0, 0);
        }
    }
}
