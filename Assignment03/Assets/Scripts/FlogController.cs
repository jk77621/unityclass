using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlogController : MonoBehaviour
{
    public float nMoveSpeed = 0.1f;
    Animator animator;
    AudioSource audio;
    Rigidbody2D rigid2D;
    string animationState = "AnimationState";
    bool bPlayer = false;

    enum States
    {
        death = 1,
        jump = 2,
        idle = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        rigid2D = GetComponent<Rigidbody2D>();

        StartCoroutine(JumpFlog());
    }

    // Update is called once per frame
    void Update()
    {
        if (bPlayer) return;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (bPlayer) return;

        switch (collision.gameObject.name)
        {
            case "player":
                float nPlayerY = collision.gameObject.transform.position.y;
                float nEagleY = gameObject.transform.position.y + (gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2);

                if (nPlayerY > nEagleY)
                {
                    bPlayer = true;
                    StartCoroutine(DeathFlog());
                }
                break;
        }
    }

    IEnumerator JumpFlog()
    {
        while (true)
        {
            animator.SetInteger(animationState, (int)States.idle);
            rigid2D.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(2.0f);

            animator.SetInteger(animationState, (int)States.jump);
            rigid2D.velocity = new Vector3(0, 0, 0);
            rigid2D.AddForce(transform.up * 200.0f);
            rigid2D.AddForce(transform.right * 100.0f);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(1.0f);

            animator.SetInteger(animationState, (int)States.jump);
            rigid2D.velocity = new Vector3(0, 0, 0);
            rigid2D.AddForce(transform.up * 200.0f);
            rigid2D.AddForce(transform.right * 100.0f);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(1.0f);

            animator.SetInteger(animationState, (int)States.jump);
            rigid2D.velocity = new Vector3(0, 0, 0);
            rigid2D.AddForce(transform.up * 200.0f);
            rigid2D.AddForce(transform.right * 100.0f);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(1.0f);

            animator.SetInteger(animationState, (int)States.idle);
            rigid2D.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(2.0f);

            animator.SetInteger(animationState, (int)States.jump);
            rigid2D.velocity = new Vector3(0, 0, 0);
            rigid2D.AddForce(transform.up * 200.0f);
            rigid2D.AddForce(-transform.right * 100.0f);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(1.0f);

            animator.SetInteger(animationState, (int)States.jump);
            rigid2D.velocity = new Vector3(0, 0, 0);
            rigid2D.AddForce(transform.up * 200.0f);
            rigid2D.AddForce(-transform.right * 100.0f);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(1.0f);

            animator.SetInteger(animationState, (int)States.jump);
            rigid2D.velocity = new Vector3(0, 0, 0);
            rigid2D.AddForce(transform.up * 200.0f);
            rigid2D.AddForce(-transform.right * 100.0f);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(1.0f);

            animator.SetInteger(animationState, (int)States.idle);
            rigid2D.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator DeathFlog()
    {
        int count = 0;
        while (true)
        {
            if (count > 1) break;

            count++;

            animator.SetInteger(animationState, (int)States.death);
            audio.Play();

            yield return new WaitForSeconds(.5f);

            Destroy(gameObject);

            yield return new WaitForSeconds(0f);
        }
    }
}
