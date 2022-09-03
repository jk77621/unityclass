using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    GameObject objCar;
    GameObject objFlag;
    GameObject objDistance;
    float nCarSpeed = 0; //�ڵ��� �ӵ�
    Vector2 vecStart;
    Vector2 vecEnd;
    int nTry = 0; //�õ� Ƚ��
    bool isPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        objCar = GameObject.Find("car");
        objFlag = GameObject.Find("flag");
        objDistance = GameObject.Find("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        nTry = int.Parse(objDistance.GetComponent<Text>().text.Substring(objDistance.GetComponent<Text>().text.Length - 1, 1));

        if (nTry == 0)
        {
            if (!isPlay) return;

            objDistance.GetComponent<Text>().text = objCar.transform.position.x > objFlag.transform.position.x - 3 && objCar.transform.position.x < objFlag.transform.position.x + 3 ? "�¸�" : "����";

            isPlay = false;
            return;
        }

        //���콺 Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            //���콺�� ��ġ��ǥ ����
            vecStart = Input.mousePosition;
            isPlay = true;
        }

        //���콺 Ŭ������ ���� ��
        else if (Input.GetMouseButtonUp(0))
        {
            //���콺�� ��ġ��ǥ ����
            vecEnd = Input.mousePosition;
            float nSwipeLength = vecEnd.x - vecStart.x;

            nCarSpeed = nSwipeLength / 500.0f;

            objCar.GetComponent<AudioSource>().Play();

            nTry--;

            objDistance.GetComponent<Text>().text = "�õ� Ƚ�� : " + nTry;
        }

        nCarSpeed *= 0.98f;

        objCar.transform.Translate(nCarSpeed, 0, 0);
    }
}
