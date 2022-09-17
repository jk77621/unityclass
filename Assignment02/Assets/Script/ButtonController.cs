using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public bool isStart = false;

    public void onClick()
    {
        isStart = true;
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
