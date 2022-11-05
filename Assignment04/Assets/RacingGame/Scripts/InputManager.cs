using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool handbrake;
    public bool boosting;

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        handbrake = Input.GetAxis("Drift") != 0 ? true : false;
        if (Input.GetKey(KeyCode.LeftControl)) boosting = true; else boosting = false;
    }
}
