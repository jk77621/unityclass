using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
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
        item = 1,
        gem = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        nStartY = gameObject.transform.position.y;
        nMaxY = nStartY + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (bPlayer) return;

        animator.SetInteger(animationState, (int)States.gem);

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
                bPlayer = true;
                StartCoroutine(DeathEagle());
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

            animator.SetInteger(animationState, (int)States.item);
            audio.Play();

            yield return new WaitForSeconds(0.5f);

            Destroy(gameObject);

            yield return new WaitForSeconds(0f);
        }
    }
}
