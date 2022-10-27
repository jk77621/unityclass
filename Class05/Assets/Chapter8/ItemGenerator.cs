using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;
    public GameObject target;
    float span = 1.0f;
    float delta = 0;
    int ratio = 2;
    float speed = -0.003f;

    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            int dice = Random.Range(1, 11);
            GameObject item = Instantiate(dice <= this.ratio ? bombPrefab : applePrefab) as GameObject;
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            item.transform.position = new Vector3(x, 4, z);
            item.GetComponent<ItemController>().dropSpeed = this.speed;

            target.transform.position = new Vector3(x, -0.1f, z);
        }
    }
}
