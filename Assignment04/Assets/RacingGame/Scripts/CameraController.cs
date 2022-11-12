using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player;
    private PlayerController RR;
    private GameObject cameraConstraint;
    private GameObject cameraLookAt;
    private float speed = 0;
    public float defaultFOV = 0, desiredFOV = 0;
    [Range(0, 50)] public float smoothTime = 8;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraConstraint = Player.transform.Find("CameraConstraint").gameObject;
        cameraLookAt = Player.transform.Find("CameraLookAt").gameObject;
        RR = Player.GetComponent<PlayerController>();
        defaultFOV = Camera.main.fieldOfView;
        desiredFOV = defaultFOV + 15;
    }

    private void FixedUpdate()
    {
        follow();
        boostFOV();
    }

    private void follow()
    {
        speed = RR.KPH / smoothTime;
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraConstraint.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameraLookAt.gameObject.transform.position);
    }

    private void boostFOV()
    {
        if (RR.nitrusFlag)
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, Time.deltaTime * 5);
        else
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaultFOV, Time.deltaTime * 5);
    }
}
