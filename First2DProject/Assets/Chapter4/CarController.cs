using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speed = 0; //�ڵ��� �ӵ�
    Vector2 startPos;
    Vector2 endPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���콺 Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            //���콺�� ��ġ��ǥ ����
            startPos = Input.mousePosition;
        }
        //���콺 Ŭ������ ���� ��
        else if (Input.GetMouseButtonUp(0))
        {
            //���콺�� ��ġ��ǥ ����
            endPos = Input.mousePosition;
            float swipeLength = endPos.x - startPos.x;

            speed = swipeLength / 500.0f;

            GetComponent<AudioSource>().Play();
        }

        transform.Translate(speed, 0, 0);

        speed *= 0.98f;
    }
}
