using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    float span = 1.0f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime; //현재 지나온 시간 = 프레임과 프레임 사이의 시간
        if (delta > span) // 1초이상 지나면 아래내용을 실행한다.
        {
            delta = 0;
            GameObject genObj = Instantiate(arrowPrefab); //게임오브젝트를 생성한다.
            int px = Random.Range(-6, 7); //-6에서 6사이의 값을 랜덤하게 출력한다.

            genObj.transform.position = new Vector3(px, 7, 0);
        }
    }
}
