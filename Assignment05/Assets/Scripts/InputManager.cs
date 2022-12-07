using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : MonoBehaviour
{
    public enum outputType
    {
        playerController,
        aiWeaponController,
        weaponController
    }
    [SerializeField] public outputType controllerType;

    private PlayerController playerController;
    private WeaponController weaponController;

    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float xValue, yValue;

    private bool pause;

    private void Start()
    {
        switch (controllerType)
        {
            case outputType.playerController:
                playerController = GetComponent<PlayerController>();
                break;

            case outputType.aiWeaponController:
                break;

            case outputType.weaponController:
                weaponController = GetComponent<WeaponController>();
                break;
        }
    }

    private void Update()
    {
        switch (controllerType)
        {
            case outputType.playerController:
                playerInput();
                break;

            case outputType.aiWeaponController:
                break;

            case outputType.weaponController:
                weaponInput();
                break;
        }
    }
    private void playerInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        xValue = Input.GetAxis("Mouse Y");
        yValue = Input.GetAxis("Mouse X");
        if (Input.GetKeyDown(KeyCode.Space)) playerController.jump();
        if (Input.GetKeyDown(KeyCode.E)) playerController.switchWeapons((playerController.weaponIndicator < 3) ? playerController.weaponIndicator + 1 : 0);
    }

    private void weaponInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        xValue = Input.GetAxis("Mouse Y");
        yValue = Input.GetAxis("Mouse X");
        if (Input.GetButton("Fire1")) weaponController.fire();
    }
}
