using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text health, amo, money;
    public GameObject[] weaponIndicator = new GameObject[4];

    public void setHealth(string i) { health.text = i; }
    public void setAmo(string i) { amo.text = i; }
    public void setMoney(string i) { money.text = i; }
    public void setWeaponToDisplay(int j)
    {
        for (int i = 0; i < weaponIndicator.Length; ++i)
        {
           weaponIndicator[i].SetActive(i == j ? true : false);
        }
    }
}
