using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;

    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("car");
        flag = GameObject.Find("flag");
        distance = GameObject.Find("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        //깃발과 차의 거리를 구하는 공식.
        float length = flag.transform.position.x - car.transform.position.x;

        distance.GetComponent<Text>().text = "남은 거리 : " + length.ToString("F2");
    }
}
