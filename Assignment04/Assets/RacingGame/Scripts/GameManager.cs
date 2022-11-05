using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VehicleList list;
    public PlayerController RR;
    public GameObject needle;
    public GameObject initPosition;
    public TextMeshProUGUI kph;
    public TextMeshProUGUI gearNum;
    private float startPosition = 32f, endPosition= -211f;
    private float desiredPosition;

    public float vehicleSpeed;

    private void Awake()
    {
        Instantiate(list.vehicles[PlayerPrefs.GetInt("pointer")], initPosition.transform.position, initPosition.transform.rotation);
        RR = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        kph.text = RR.KPH.ToString("0");
        updateNeedle();
    }

    public void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        float temp = RR.engineRPM / 10000;
        needle.transform.eulerAngles = new Vector3(0, 0,(startPosition - temp * desiredPosition));
    }

    public void changeGear()
    {
        gearNum.text = RR.reverse ? "R" : RR.gearNum.ToString("0");
    }
}
