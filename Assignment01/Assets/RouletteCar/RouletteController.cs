using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour
{
    GameObject objRoulette;
    GameObject objDistance;
    float nRotSpeed = 0; //ȸ�� �ӵ�
    float nZ = 0;
    bool isPlay = false;
    int nTry = 0; //�õ� Ƚ��

    // Start is called before the first frame update
    void Start()
    {
        objRoulette = GameObject.Find("roulette");
        objDistance = GameObject.Find("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        if (nTry != 0) return;

        //Ŭ���ϸ� ȸ���Ѵ�.
        if (Input.GetMouseButton(0))
        {
            nRotSpeed = 10;
            isPlay = true;
        }

        if (!isPlay) return;

        //ȸ���ӵ��� ���δ�.
        nRotSpeed *= 0.95f;

        //ȸ���ӵ���ŭ �귿�� ȸ����Ų��.
        objRoulette.transform.Rotate(0, 0, nRotSpeed);

        if (nRotSpeed < 0.001f)
        {
            nRotSpeed = 0;
            nZ = objRoulette.transform.eulerAngles.z;

            if ((nZ >= 0 && nZ <= 30) || (nZ >= 331 && nZ <= 360))
            {
                Debug.Log("��� ����");
                nTry = 1;
            }
            else if (nZ >= 31 && nZ <= 90)
            {
                Debug.Log("��� ����");
                nTry = 2;
            }
            else if (nZ >= 91 && nZ <= 150)
            {
                Debug.Log("��� �ſ� ����");
                nTry = 1;
            }
            else if (nZ >= 151 && nZ <= 210)
            {
                Debug.Log("��� ����");
                nTry = 3;
            }
            else if (nZ >= 211 && nZ <= 270)
            {
                Debug.Log("��� ����");
                nTry = 2;
            }
            else if (nZ >= 271 && nZ <= 330)
            {
                Debug.Log("��� ����");
                nTry = 4;
            }

            objDistance.GetComponent<Text>().text = "�õ� Ƚ�� : " + nTry;

            isPlay = false;
        }
    }
}
