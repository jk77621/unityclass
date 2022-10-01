using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform upLog;
    public Transform downLog;
    public bool isDownObstacle = false; //아래 방해물인지 체크
    public float speed;
    private bool alreadyCreated = false;
    private bool gotPoint = false;
    private GameObject bird;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bird = GameObject.Find("Player");

        body.velocity = Vector2.up * speed;
        InvokeRepeating("Switch", 0, 2); // 이동 방향 변경
    }

    // Update is called once per frame
    void Update()
    {
        float birdX = bird.transform.position.x;

        //위에 있는 방해물이 일정거리만큼 가면 새로 생성
        if (isDownObstacle)
        {
            if (GetComponent<Renderer>().isVisible && !alreadyCreated)
            {
                CreateNextObstacles();
                alreadyCreated = true;
            }

            //유저가 해당 위치까지 왔으면
            if (!gotPoint && transform.position.x < birdX)
            {
                //점수 증가
                LevelManager.instance.UpdatePoints();
                gotPoint = true;
            }
        }

        //뒤로 넘어가면 삭제
        if (transform.position.x < birdX - 6)
        {
            Destroy(gameObject);
        }
    }

    void CreateNextObstacles()
    {
        //위치 랜덤 지정
        float nextX = transform.position.x + Random.Range(6, 10);
        float targetY = Random.Range(11f, -2.5f);
        float deltaY = Random.Range(10f, 11f);

        //위치 지정
        Vector3 upLogPosition = new Vector3(nextX, targetY + deltaY, transform.position.z);
        Vector3 downLogPosition = new Vector3(nextX, targetY - deltaY, transform.position.z);

        Transform upLogObject = Instantiate(upLog, upLogPosition, Quaternion.identity);
        Transform downLogObject = Instantiate(downLog, downLogPosition, Quaternion.identity);

        //움직일지 아닐지 체크함 (랜덤)
        bool shouldMove = Random.Range(1, 10) % 3 == 0;
        if (shouldMove)
        {
            upLogObject.GetComponent<Obstacle>().speed = 1;
            downLogObject.GetComponent<Obstacle>().speed = 1;
        }
        else
        {
            upLogObject.GetComponent<Obstacle>().speed = 0;
            downLogObject.GetComponent<Obstacle>().speed = 0;
        }
    }

    void Switch()
    {
        body.velocity *= -1;
    }
}
