using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    public float nMoveSpeed = 0.1f;
    Animator animator;
    AudioSource audio;
    string animationState = "AnimationState";
    float nStartY;
    float nMaxY;
    bool bPlayer = false;

    enum States
    {
        death = 1,
        idle = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        nStartY = gameObject.transform.position.y;
        nMaxY = nStartY + 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (bPlayer) return;

        animator.SetInteger(animationState, (int)States.idle);

        gameObject.transform.Translate(0, nMoveSpeed, 0);

        if (gameObject.transform.position.y > nMaxY || gameObject.transform.position.y < nStartY)
        {
            nMoveSpeed = -nMoveSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "player":
                float nPlayerY = collision.gameObject.transform.position.y;
                float nEagleY = gameObject.transform.position.y;

                if (nPlayerY > nEagleY)
                {
                    bPlayer = true;
                    StartCoroutine(DeathEagle());
                }
                break;
        }
    }

    IEnumerator DeathEagle()
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

