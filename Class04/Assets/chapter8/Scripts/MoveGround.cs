using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float speed = 1f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //정 위치를 지나치면 맨 뒤로 이동하는 알고리즘

        float targetEnd = target.position.x - 48;
        float targetOrigin = target.position.x + 48;

        if (transform.position.x < targetEnd)
        {
            transform.position = new Vector3(targetOrigin, transform.position.y, transform.position.z);
        }
    }
}
