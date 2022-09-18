using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject arrow;
    Rigidbody2D rigidbody2D;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }

        if (Input.GetKeyDown(KeyCode.A) && key != 0)
        {
            GameObject genObj = Instantiate(arrow);
            genObj.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            genObj.transform.rotation = Quaternion.Euler(0, 0, key == 1 ? 90 : 270);
        }

        float speedX = Mathf.Abs(rigidbody2D.velocity.x);
        if (speedX < maxWalkSpeed)
        {
            rigidbody2D.AddForce(transform.right * key * walkForce);
        }

        if (key != 0)
        {
            transform.localScale = new Vector3(-key, 1, 1);
        }
    }
}
