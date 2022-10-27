using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    GameObject timerText;
    GameObject pointText;
    GameObject generator;
    GameObject light;
    float time = 30.0f;
    int point = 0;

    public void GetApple()
    {
        point += 100;
    }

    public void GetBomb()
    {
        if (point < 0)
        {
            point /= 2;
        }
        else
        {
            point *= 2;
        }
    }

    public void GetEmpty()
    {
        point -= 100;
    }

    private void Start()
    {
        timerText = GameObject.Find("Time");
        pointText = GameObject.Find("Point");
        generator = GameObject.Find("ItemGenerator");
        light = GameObject.Find("Directional Light");
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (this.time < 0)
        {
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 0);
        }
        else if (this.time >= 0 && this.time < 5)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.9f, -0.009f, 0);
        }
        else if (this.time >= 5 && this.time < 10)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.4f, -0.006f, 0);
        }
        else if (this.time >= 10 && this.time < 20)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.4f, -0.006f, 0);
        }
        else if (this.time >= 20 && this.time < 30)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.003f, 0);
        }

        this.light.transform.rotation = Quaternion.Euler(time * 3, 0, 0);

        timerText.GetComponent<TMP_Text>().text = time.ToString("F1");
        pointText.GetComponent<TMP_Text>().text = point.ToString() + " point";
    }
}
