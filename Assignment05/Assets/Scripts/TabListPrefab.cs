using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabListPrefab : MonoBehaviour
{
    public Text playerName, kills, deaths;

    public void setUpPlayerList(string name, int kill, int death)
    {
        playerName.text = name;
        kills.text = kill.ToString();
        deaths.text = death.ToString();
    }
}
