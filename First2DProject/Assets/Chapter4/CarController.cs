using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speed = 0; //자동차 속도
    Vector2 startPos;
    Vector2 endPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //마우스 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            //마우스의 위치좌표 저장
            startPos = Input.mousePosition;
        }
        //마우스 클릭에서 땠을 때
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스의 위치좌표 저장
            endPos = Input.mousePosition;
            float swipeLength = endPos.x - startPos.x;

            speed = swipeLength / 500.0f;

            GetComponent<AudioSource>().Play();
        }

        transform.Translate(speed, 0, 0);

        speed *= 0.98f;
    }
}
