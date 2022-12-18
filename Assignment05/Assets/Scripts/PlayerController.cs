using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("animator")]
    public Animator animator;

    public string playerName = "Player";
    public float health = 100f;

    private InputManager inputManager;
    private CharacterController characterController;
    public GameObject cameraObject;
    public GameObject spine;
    public UiManager uiManager;

    public PlayerController playerController;

    public int weaponIndicator;
    public GameObject[] weapons = new GameObject[4];

    public float jumpForce = 200f, movementSpeed = 12f, gravityForce = -9.81f;
    [Range(0.1f, 5f)] public float mouseSensitivity = 0.7f;

    private Vector3 movementVector, gravity;
    private WeaponController currentWeapon;
    private GameManager gameManager;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();

        if (gameObject.layer == 6) playerController.enabled = false;

        uiManager = GameObject.FindGameObjectWithTag("UISystem").GetComponent<UiManager>();
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();

        switchWeapons(0);

        uiManager.setHealth("100");
        uiManager.setMoney("$100");
        uiManager.setWeaponToDisplay(0);
        uiManager.setAmo("0/0");
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", inputManager.vertical);
        animator.SetFloat("Direction", inputManager.horizontal);



        movementVector = transform.right * inputManager.horizontal + transform.forward * inputManager.vertical;
        characterController.Move(movementVector * movementSpeed * Time.deltaTime);
        if (isGrounded() && gravity.y < 0)
            gravity.y = -2;

        gravity.y += gravityForce * Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);



        transform.localRotation *= Quaternion.Euler(0f, inputManager.yValue * mouseSensitivity, 0f);
        if (inputManager.xValue > 0 && cameraObject.transform.localRotation.x > -0.7f)
        {
            cameraObject.transform.localRotation *= Quaternion.Euler(-inputManager.xValue * mouseSensitivity, 0f, 0f);
            spine.transform.localRotation *= Quaternion.Euler((-inputManager.xValue * mouseSensitivity / 2), 0f, 0f);
        }
        if (inputManager.xValue < 0 && cameraObject.transform.localRotation.x < 0.7f)
        {
            cameraObject.transform.localRotation *= Quaternion.Euler(-inputManager.xValue * mouseSensitivity, 0f, 0f);
            spine.transform.localRotation *= Quaternion.Euler((-inputManager.xValue * mouseSensitivity / 2), 0f, 0f);
        }



        try
        {
            currentWeapon.isGrounded = isGrounded();
        }
        catch { }

        uiManager.setHealth(health + "");
        uiManager.setAmo(currentWeapon.magazine + "/" + currentWeapon.amo);
    }

    public void jump()
    {
        if (isGrounded())
            gravity.y = Mathf.Sqrt(jumpForce * -2 * gravityForce);
    }

    private bool isGrounded()
    {
        RaycastHit raycastHit;
        if (Physics.SphereCast(transform.position, characterController.radius * (1.0f - 0), Vector3.down, out raycastHit,
            ((characterController.height / 2f) - characterController.radius) + 0.1f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        else return false;
    }

    private void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUILayout.Label("grounded : " + isGrounded());
    }

    public void switchWeapons(int j)
    {
        for (int i = 0; i < weapons.Length; ++i)
        {
            weapons[i].SetActive(i == j ? true : false);
        }
        uiManager.setWeaponToDisplay(j);
        weaponIndicator = j;
        currentWeapon = weapons[j].transform.GetChild(0).gameObject.GetComponent<WeaponController>();
    }

    public void die(GameObject shooter)
    {
        gameManager.deadPlayer(shooter, gameObject);
        //Destroy(gameObject);
    }
}
