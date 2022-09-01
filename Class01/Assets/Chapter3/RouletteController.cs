using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; //ȸ�� �ӵ�
    float nZ = 0;
    bool isPlay = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Ŭ���ϸ� ȸ���Ѵ�.
        if (Input.GetMouseButtonDown(0))
        {
            rotSpeed = 10;
            isPlay = true;
        }

        if (!isPlay) return;

        //ȸ���ӵ��� ���δ�.
        rotSpeed *= 0.95f;

        //ȸ���ӵ���ŭ �귿�� ȸ����Ų��.
        gameObject.transform.Rotate(0, 0, rotSpeed);

        if (rotSpeed < 0.001f)
        {
            rotSpeed = 0;
            nZ = gameObject.transform.eulerAngles.z;

            if ((nZ >= 0 && nZ <= 30) || (nZ >= 331 && nZ <= 360))
            {
                Debug.Log("��� ����");
            }
            else if (nZ >= 31 && nZ <= 90)
            {
                Debug.Log("��� ����");
            }
            else if (nZ >= 91 && nZ <= 150)
            {
                Debug.Log("��� �ſ� ����");
            }
            else if (nZ >= 151 && nZ <= 210)
            {
                Debug.Log("��� ����");
            }
            else if (nZ >= 211 && nZ <= 270)
            {
                Debug.Log("��� ����");
            }
            else if (nZ >= 271 && nZ <= 330)
            {
                Debug.Log("��� ����");
            }

            isPlay = false;
        }
    }
}
