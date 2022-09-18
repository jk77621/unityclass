using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isHit = false;
    public int count = 0;
    public float arrowSpeed = 3.0f;
    GameObject hpGauge;
    GameObject hp1Gauge;
    GameObject damage;
    GameObject player;
    float delta = 0f;
    float span = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        hpGauge = GameObject.Find("hp");
        hp1Gauge = GameObject.Find("hp1");
        damage = GameObject.Find("damage");
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            delta += Time.deltaTime;
            if (delta > span)
            {
                delta = 0;

                player.GetComponent<SpriteRenderer>().enabled = count % 2 == 0;

                if (count == 10)
                {
                    count = 0;
                    isHit = false;

                    if (Mathf.Ceil(hp1Gauge.GetComponent<Image>().fillAmount) < 0.1f)
                    {
                        damage.GetComponent<Text>().text = "Game Over";
                    }
                    else
                    {
                        damage.GetComponent<Text>().text = "";
                    }
                }
                else
                {
                    count++;
                }
            }
        }
    }

    //플레이어 체력 감소
    public void DecreaseHp()
    {
        hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        if (Mathf.Ceil(hp1Gauge.GetComponent<Image>().fillAmount) < 0.1f)
        {
            damage.GetComponent<Text>().text = "Game Over";
            return;
        }

        damage.GetComponent<Text>().text = Mathf.Ceil(hpGauge.GetComponent<Image>().fillAmount * 100).ToString();
    }

    //보스 체력 감소
    public void DecreaseHp1()
    {
        hp1Gauge.GetComponent<Image>().fillAmount -= 0.1f;

        if (Mathf.Ceil(hp1Gauge.GetComponent<Image>().fillAmount) < 0.1f)
        {
            damage.GetComponent<Text>().text = "Game Over";
            return;
        }

        if (Mathf.Ceil(hp1Gauge.GetComponent<Image>().fillAmount) < 0.4f)
        {
            arrowSpeed = 6.0f;
        }
    }
}
