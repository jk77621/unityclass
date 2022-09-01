using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour
{
    GameObject objRoulette;
    GameObject objDistance;
    float nRotSpeed = 0; //회전 속도
    float nZ = 0;
    bool isPlay = false;
    int nTry = 0; //시도 횟수

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

        //클릭하면 회전한다.
        if (Input.GetMouseButton(0))
        {
            nRotSpeed = 10;
            isPlay = true;
        }

        if (!isPlay) return;

        //회전속도를 줄인다.
        nRotSpeed *= 0.95f;

        //회전속도만큼 룰렛을 회전시킨다.
        objRoulette.transform.Rotate(0, 0, nRotSpeed);

        if (nRotSpeed < 0.001f)
        {
            nRotSpeed = 0;
            nZ = objRoulette.transform.eulerAngles.z;

            if ((nZ >= 0 && nZ <= 30) || (nZ >= 331 && nZ <= 360))
            {
                Debug.Log("운수 나쁨");
                nTry = 1;
            }
            else if (nZ >= 31 && nZ <= 90)
            {
                Debug.Log("운수 대통");
                nTry = 2;
            }
            else if (nZ >= 91 && nZ <= 150)
            {
                Debug.Log("운수 매우 나쁨");
                nTry = 1;
            }
            else if (nZ >= 151 && nZ <= 210)
            {
                Debug.Log("운수 보통");
                nTry = 3;
            }
            else if (nZ >= 211 && nZ <= 270)
            {
                Debug.Log("운수 조심");
                nTry = 2;
            }
            else if (nZ >= 271 && nZ <= 330)
            {
                Debug.Log("운수 좋음");
                nTry = 4;
            }

            objDistance.GetComponent<Text>().text = "시도 횟수 : " + nTry;

            isPlay = false;
        }
    }
}
