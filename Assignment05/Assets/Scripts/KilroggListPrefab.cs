using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KilroggListPrefab : MonoBehaviour
{
    public Text shooter, hitPerson;

    public void setUpKilroggList(string shooterName, string hitPersonName)
    {
        shooter.text = shooterName;
        hitPerson.text = hitPersonName;
    }
}
