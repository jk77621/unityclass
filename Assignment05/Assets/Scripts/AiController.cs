using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{

    [Header("animator")]
    public Animator animator;

    [Header("AIInputs")]
    public float verticalInput = 0;

    public string playerName = "Ai";
    public float health = 100f;

    public PlayerController playerController;
    public NavMeshAgent agent;
    public weaponCotrollerAI weaponControllerAI;
    public GameObject aimPoint;

    private CharacterController characterController;
    public GameObject cameraObject;

    public float jumpForce = 200f, movementSpeed = 12, gravityForce = -9.81f;
    [Range(0.1f, 5f)] public float mouseSensitivity = 0.7f;

    private Vector3 movementVector, gravity;

    public float campMaxDistance = 10;
    private float localMaxDistance;
    public float obstacleDistance = 0;
    public GameObject rayCastPoint;

    // AI values
    public float forwardMovement, rightMovement;
    public float mouseX, mouseY;

    // camp points
    public float campPointDistance;
    public campPint[] campPints;
    private Transform destination;
    public bool camping = false;
    public float startTimer = 5;

    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        campPints = FindObjectsOfType<campPint>();

        characterController = GetComponent<CharacterController>();
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        localMaxDistance = campMaxDistance;

        startTimer = Time.time + startTimer;
    }

    public float sensorDistance = 100, playerCurrentDistance = 0;

    void Update()
    {
        if (playerController == null) playerController = FindObjectOfType<PlayerController>();

        aimingMethod();
    }

    private void FixedUpdate()
    {
        //if(Time.time <= startTimer)return;
        if (playerController == null) return;

        aim();

        if (!aiming)
        {

            if (!camping)
            {
                Roam();
            }
            /*
            camp();
            if(camping ){
                agent.SetDestination(destination.transform.position);
                if(Vector3.Distance(transform.position , destination.transform.position ) <= 2)
                verticalInput = 0;
            }
            */
        }
        else
        {
            verticalInput = 0;
        }

        animator.SetFloat("Speed", verticalInput);
        //animator.SetFloat("Direction",inputManager.horizontal );
        playerCurrentDistance = Vector3.Distance(transform.position, playerController.transform.position);

        /*
        if(playerCurrentDistance < sensorDistance){
            agent.SetDestination(playerController.transform.position);
            verticalInput = 1;
        }else{
            verticalInput = 0;
        }*/
        agent.speed = verticalInput;




        movementVector = transform.right * rightMovement + transform.forward * forwardMovement;
        characterController.Move(movementVector * movementSpeed * Time.deltaTime);
        if (isGrounded() && gravity.y < 0)
            gravity.y = -2;

        gravity.y += gravityForce * Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);




        transform.localRotation *= Quaternion.Euler(0f, mouseY * mouseSensitivity, 0f);
        if (mouseX > 0 && cameraObject.transform.localRotation.x > -0.7f)
        {
            cameraObject.transform.localRotation *= Quaternion.Euler(-mouseX * mouseSensitivity, 0f, 0f);
            //spine.transform.localRotation *= Quaternion.Euler((-mouseX * mouseSensitivity / 2), 0f , 0f);
        }
        if (mouseX < 0 && cameraObject.transform.localRotation.x < 0.7f)
        {
            cameraObject.transform.localRotation *= Quaternion.Euler(-mouseX * mouseSensitivity, 0f, 0f);
            //spine.transform.localRotation *= Quaternion.Euler((-mouseX * mouseSensitivity / 2), 0f , 0f);
        }

        try
        {
            weaponControllerAI.isGrounded = isGrounded();
        }
        catch { }
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
                                ((characterController.height / 2f) - characterController.radius) + .1f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        else return false;
    }

    public void die(GameObject shooter)
    {
        gameManager.deadPlayer(shooter, gameObject);
        Destroy(gameObject);
    }

    public void Roam()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayCastPoint.transform.position, rayCastPoint.transform.forward, out hit))
        {
            obstacleDistance = hit.distance;
            Debug.DrawRay(rayCastPoint.transform.position, rayCastPoint.transform.forward, Color.green);
        }


        verticalInput = (obstacleDistance >= campMaxDistance && !aiming) ? 1 : 0;

        if (verticalInput == 0)
        {
            if (!directionFlag)
                setDirection();
            obstacleDistance = (Direction) ? obstacleDistance : -obstacleDistance;
            mouseY = obstacleDistance * 10;

        }
        else
        {
            directionFlag = false;
            mouseY = 0;
            campMaxDistance = localMaxDistance;
        }

    }

    private bool Direction;
    private bool directionFlag;

    public void setDirection()
    {
        campMaxDistance += campMaxDistance / 2;
        directionFlag = true;
        Direction = (Random.Range(0, 2) == 1) ? true : false;
    }

    public void camp()
    {
        for (int i = 0; i < campPints.Length; i++)
        {
            if (Vector3.Distance(transform.position, campPints[i].transform.position) <= campPointDistance)
            {
                //agent.SetDestination(campPints[i].transform.position);
                verticalInput = .7f;
                destination = campPints[i].transform;
                camping = true;
                forwardMovement = 0;
                mouseY = 0;
            }
        }
    }

    public bool aiming;
    private float aimTimer, aimFlag;
    public string rayInfo, rayInfoSubed;


    public string test;

    public void aim()
    {
        aimPoint.transform.LookAt(playerController.transform.position);

        Debug.DrawRay(aimPoint.transform.position, aimPoint.transform.forward, Color.black);

        RaycastHit raycastHit;
        if (Physics.Raycast(aimPoint.transform.position, aimPoint.transform.forward, out raycastHit, Mathf.Infinity))
        {


            rayInfo = raycastHit.transform.name;
            if (rayInfo.Length >= 4)
                rayInfoSubed = rayInfo.Substring(0, 4);
        }


        if (rayInfoSubed == "swat")
        {
            agent.isStopped = true;
            forwardMovement = 0;
            mouseY = 0;
            if (raycastHit.transform.root.tag == "Player")
            {
                aiming = true;
                aimTimer = Time.time + 1;
            }

        }
        else
        {
            if (Time.time > aimTimer)
                aiming = false;
        }


    }

    void aimingMethod()
    {
        if (aiming)
        {
            agent.isStopped = true;
            camping = false;
            forwardMovement = 0;
            mouseY = 0;
            weaponControllerAI.fire();
            transform.LookAt(playerController.transform.position);

        }
    }


}

