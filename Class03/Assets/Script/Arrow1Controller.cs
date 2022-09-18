using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow1Controller : MonoBehaviour
{
    public float arrowSpeed = 0f;
    GameObject player;
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        arrowSpeed = gameManager.GetComponent<GameManager>().arrowSpeed;

        gameObject.transform.Translate(Vector3.down * arrowSpeed * Time.deltaTime);

        Vector2 p1 = gameObject.transform.position;
        Vector2 p2 = player.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;

        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d <= r1 + r2)
        {
            Debug.Log("Player Hit");

            gameManager.GetComponent<GameManager>().isHit = true;
            gameManager.GetComponent<GameManager>().count = 0;
            gameManager.GetComponent<GameManager>().DecreaseHp();

            Destroy(gameObject);
        }
    }
}
