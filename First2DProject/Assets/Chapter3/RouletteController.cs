using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; //회전 속도
    float nZ = 0;
    bool isPlay = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //클릭하면 회전한다.
        if (Input.GetMouseButtonDown(0))
        {
            rotSpeed = 10;
            isPlay = true;
        }

        if (!isPlay) return;

        //회전속도를 줄인다.
        rotSpeed *= 0.95f;

        //회전속도만큼 룰렛을 회전시킨다.
        gameObject.transform.Rotate(0, 0, rotSpeed);

        if (rotSpeed < 0.001f)
        {
            rotSpeed = 0;
            nZ = gameObject.transform.eulerAngles.z;

            if ((nZ >= 0 && nZ <= 30) || (nZ >= 331 && nZ <= 360))
            {
                Debug.Log("운수 나쁨");
            }
            else if (nZ >= 31 && nZ <= 90)
            {
                Debug.Log("운수 대통");
            }
            else if (nZ >= 91 && nZ <= 150)
            {
                Debug.Log("운수 매우 나쁨");
            }
            else if (nZ >= 151 && nZ <= 210)
            {
                Debug.Log("운수 보통");
            }
            else if (nZ >= 211 && nZ <= 270)
            {
                Debug.Log("운수 조심");
            }
            else if (nZ >= 271 && nZ <= 330)
            {
                Debug.Log("운수 좋음");
            }

            isPlay = false;
        }
    }
}
