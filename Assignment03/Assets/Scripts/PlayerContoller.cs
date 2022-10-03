using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerContoller : MonoBehaviour
{
    GameObject scaff;
    TilemapCollider2D collider2D;
    Rigidbody2D rigid2D;
    Animator animator;
    Renderer renderer;
    string animationState = "AnimationState";
    float nJumpForce = 680.0f;
    float nWalkForce = 40.0f;
    float nMaxWalkSpeed = 2.0f;
    int nKey = 0;
    bool bEnemy = false;
    bool bLadder = false;

    enum States
    {
        jump = 1,
        run = 2,
        hurt = 3,
        climb = 4,
        idle = 5
    }

    // Start is called before the first frame update
    void Start()
    {
        scaff = GameObject.Find("Scaff");
        collider2D = scaff.GetComponent<TilemapCollider2D>();
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bEnemy) return;

        if (bLadder)
        {
            //사다리이동
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.transform.Translate(0, 0.02f, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                gameObject.transform.Translate(0, -0.02f, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Translate(-0.02f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Translate(0.02f, 0, 0);
            }
        }
        else
        {
            //점프
            if (Input.GetKeyDown(KeyCode.Space) && rigid2D.velocity.y == 0)
            {
                rigid2D.AddForce(transform.up * nJumpForce);
            }

            //좌우이동
            nKey = 0;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                nKey = 1;
                animator.SetInteger(animationState, (int)States.run);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                nKey = -1;
                animator.SetInteger(animationState, (int)States.run);
            }
            else
            {
                if (rigid2D.velocity.y == 0)
                {
                    animator.SetInteger(animationState, (int)States.idle);
                }
                else
                {
                    animator.SetInteger(animationState, (int)States.jump);
                }
            }

            //플레이어 속도 체크
            float speedx = Mathf.Abs(rigid2D.velocity.x);

            //스피드 제한
            if (speedx < nMaxWalkSpeed)
            {
                rigid2D.AddForce(transform.right * nKey * nWalkForce);
            }

            //움직이는 방향 전환
            if (nKey != -0)
            {
                transform.localScale = new Vector3(nKey, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bEnemy) return;

        switch (collision.gameObject.name)
        {
            case "Ladder":
                bLadder = true;

                rigid2D.gravityScale = 0;
                rigid2D.velocity = new Vector3(0, 0, 0);

                collider2D.isTrigger = true;

                animator.SetInteger(animationState, (int)States.climb);
                break;
            case "eagle":
            case "eagle1":
            case "opossum":
            case "opossum1":
            case "flog":
            case "flog1":
                float nEagleX = collision.gameObject.transform.position.x;
                float nEagleY = collision.gameObject.transform.position.y + (collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2);
                float nPlayerX = gameObject.transform.position.x;
                float nPlayerY = gameObject.transform.position.y;

                bEnemy = true;

                if (nPlayerY > nEagleY)
                {
                    StartCoroutine(BouncePlayer());
                    StartCoroutine(BlinkPlayer());
                }
                else
                {
                    nKey = 0;
                    nKey = nPlayerX > nEagleX ? 1 : -1;

                    StartCoroutine(BouncePlayer1());
                    StartCoroutine(BlinkPlayer1());
                }
                break;
            case "house":
                EndGame();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Ladder":
                bLadder = false;

                rigid2D.gravityScale = 3;

                collider2D.isTrigger = false;
                break;
        }
    }

    IEnumerator BouncePlayer()
    {
        rigid2D.velocity = new Vector3(0, 0, 0);
        rigid2D.AddForce(transform.up * (nJumpForce / 2));
        rigid2D.AddForce(transform.right * nKey * (nJumpForce / 2));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator BlinkPlayer()
    {
        int count = 0;
        while (true)
        {
            if (count > 1)
            {
                bEnemy = false;
                break;
            }

            count++;

            yield return new WaitForSeconds(.1f);

            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator BouncePlayer1()
    {
        rigid2D.velocity = new Vector3(0, 0, 0);
        rigid2D.AddForce(transform.up * (nJumpForce / 2));
        rigid2D.AddForce(transform.right * nKey * (nJumpForce / 2));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator BlinkPlayer1()
    {
        int count = 0;
        while (true)
        {
            if (count > 1)
            {
                bEnemy = false;
                break;
            }

            count++;

            renderer.enabled = false;
            animator.SetInteger(animationState, (int)States.hurt);
            yield return new WaitForSeconds(.5f);

            renderer.enabled = true;
            animator.SetInteger(animationState, (int)States.hurt);
            yield return new WaitForSeconds(.5f);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
