using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject toRotate;
    public GameObject buyButton;
    public GameObject startButton;
    public float rotateSpeed;
    public VehicleList listOfVehicle;
    public int vehiclePointer = 0;
    public TextMeshProUGUI currency;
    public TextMeshProUGUI carInfo;

    private void Awake()
    {
        //PlayerPrefs Init
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("pointer", 0);
        PlayerPrefs.SetInt("currency", 100000);

        vehiclePointer = PlayerPrefs.GetInt("pointer");

        GameObject childObject = Instantiate(listOfVehicle.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
        childObject.transform.parent = toRotate.transform;
        getCarInfo();
    }

    private void FixedUpdate()
    {
        toRotate.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    public void rightButton()
    {
        if (vehiclePointer < listOfVehicle.vehicles.Length - 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            GameObject childObject = Instantiate(listOfVehicle.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = toRotate.transform;
            getCarInfo();
        }
    }

    public void leftButton()
    {
        if (vehiclePointer > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            GameObject childObject = Instantiate(listOfVehicle.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = toRotate.transform;
            getCarInfo();
        }
    }

    public void startGameButton()
    {
        SceneManager.LoadScene("main");
    }

    public void buyGameButton()
    {

        if (PlayerPrefs.GetInt("currency") >= listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carPrice)
        {
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") - listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carPrice);
            PlayerPrefs.SetString(listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carName.ToString(),
                        listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carName.ToString());
            getCarInfo();
        }
    }

    public void getCarInfo()
    {
        if (listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carName.ToString() ==
            PlayerPrefs.GetString(listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carName.ToString()))
        {
            carInfo.text = "Owned";
            startButton.SetActive(true);
            buyButton.SetActive(false);
            currency.text = PlayerPrefs.GetInt("currency").ToString("");
        }
        else
        {
            carInfo.text = listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carName.ToString() + " " +
                            listOfVehicle.vehicles[vehiclePointer].GetComponent<PlayerController>().carPrice.ToString();

            startButton.SetActive(false);
            buyButton.SetActive(buyButton);
            currency.text = PlayerPrefs.GetInt("currency").ToString("");
        }
    }
}
