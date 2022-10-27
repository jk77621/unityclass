using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidBody;
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.UpArrow) == true)
        //{
        //    playerRigidBody.AddForce(0f, 0f, speed);
        //}

        //if (Input.GetKey(KeyCode.DownArrow) == true)
        //{
        //    playerRigidBody.AddForce(0f, 0f, -speed);
        //}

        //if (Input.GetKey(KeyCode.RightArrow) == true)
        //{
        //    playerRigidBody.AddForce(speed, 0f, 0f);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow) == true)
        //{
        //    playerRigidBody.AddForce(-speed, 0f, 0f);
        //}

        //����� ������ �Է°��� ���� �� ����
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //���� �̵� �ӵ��� �Է� ���� �̵� �ӷ��� ���� ����
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        //Vec3 �ӵ��� ����
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        //Rigidbody�� �Ҵ�
        playerRigidBody.velocity = newVelocity;
    }

    public void Die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();

        gameManager.EndGame();
    }
}
