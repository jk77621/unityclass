using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    GameObject objCar;
    GameObject objDistance;
    float nCarSpeed = 0; //자동차 속도
    Vector2 vecStart;
    Vector2 vecEnd;
    int nTry = 0; //시도 횟수

    // Start is called before the first frame update
    void Start()
    {
        objCar = GameObject.Find("car");
        objDistance = GameObject.Find("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        nTry = int.Parse(objDistance.GetComponent<Text>().text.Substring(objDistance.GetComponent<Text>().text.Length - 1, 1));

        if (nTry == 0) return;

        //마우스 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            //마우스의 위치좌표 저장
            vecStart = Input.mousePosition;
        }

        //마우스 클릭에서 땠을 때
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스의 위치좌표 저장
            vecEnd = Input.mousePosition;
            float nSwipeLength = vecEnd.x - vecStart.x;

            nCarSpeed = nSwipeLength / 500.0f;

            objCar.GetComponent<AudioSource>().Play();

            nTry--;

            objDistance.GetComponent<Text>().text = "시도 횟수 : " + nTry;
        }

        nCarSpeed *= 0.98f;

        objCar.transform.Translate(nCarSpeed, 0, 0);
    }
}
