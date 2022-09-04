using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowController : MonoBehaviour
{
    //고양이
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");

    }

    // Update is called once per frame
    void Update()
    {
        //프레임마다 등속으로 낙하시킨다.
        gameObject.transform.Translate(0, -0.1f, 0);

        //화면 밖으로 나가면 소멸한다.
        if (gameObject.transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        //충돌판정
        Vector2 p1 = gameObject.transform.position;
        Vector2 p2 = player.transform.position;

        Vector2 dir = p1 - p2; //벡터의 방향을 알려줌

        float d = dir.magnitude; //벡터의 길이를 반환해줌

        float r1 = 0.5f; //화살표의 반경
        float r2 = 1.0f; //플레이어의 반경

        if (d <= r1 + r2)
        {
            GameObject director = GameObject.Find("GameManager");
            director.GetComponent<GameManager>().DecreaseHp();

            Destroy(gameObject);
        }
    }
}
