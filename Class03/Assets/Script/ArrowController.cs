using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject boss;
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("boss");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.down * 3.0f * Time.deltaTime);

        Vector2 p1 = gameObject.transform.position;
        Vector2 p2 = boss.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;

        float r1 = 0.5f;
        float r2 = 2.0f;

        if (d <= r1 + r2)
        {
            Debug.Log("Boss Hit");

            gameManager.GetComponent<GameManager>().DecreaseHp1();

            //arrow1Controller.GetComponent<Arrow1Controller>().arrowSpeed = 6.0f;

            Destroy(gameObject);
        }
    }
}
