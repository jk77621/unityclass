using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField] private driveType drive;

    internal enum gearBox
    {
        automatic,
        manual
    }
    [SerializeField] private gearBox gearChange;

    [Header("Variables")]
    public float handBreakFrictionMultiplier = 2f;
    public float totalPower;
    public float maxRPM, minRPM;
    public float KPH;
    public float wheelsRPM;
    public float engineRPM;
    public float[] gears;
    public int gearNum = 0;
    public bool reverse = false;
    public AnimationCurve enginePower;

    [HideInInspector] public bool playPauseSmoke = false;
    [HideInInspector] public bool test;

    private GameManager manager;
    private InputManager IM;
    private GameObject meshes, colliders;
    private WheelCollider[] wheels = new WheelCollider[4];
    private GameObject[] wheelMesh = new GameObject[4];
    private GameObject centerOfMass;
    private Rigidbody rigidbody;

    public int carPrice;
    public string carName;

    private WheelFrictionCurve forwardFriction, sidewaysFriction;
    private float thrust = 20000f, radius = 5, breakPower = 50000, DownForceValue = 100f, smoothTime = 0.09f;

    [Header("Debug")]
    public float[] slip = new float[4];

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Select") return;

        getObjects();
        StartCoroutine(timedLoop());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Select") return;

        addDownForce();
        animateWheels();
        steerVehicle();
        //getFriction();
        calculateEnginePower();
        adjustFriction();
    }

    private void calculateEnginePower()
    {
        wheelRPM();

        totalPower = enginePower.Evaluate(engineRPM) * (gears[gearNum]) * IM.vertical;
        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);

        moveVehicle();
        shifter();
    }

    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; ++i)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;

        if (wheelsRPM < 0 && !reverse)
        {
            reverse = true;
            manager.changeGear();
        }
        else if (wheelsRPM > 0 && reverse)
        {
            reverse = false;
            manager.changeGear();
        }
    }

    private void shifter()
    {
        if (!isGrounded()) return;

        if (gearChange == gearBox.automatic)
        {
            if (engineRPM > maxRPM && gearNum < gears.Length - 1 && !reverse)
            {
                gearNum++;
                manager.changeGear();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(gearNum);
                gearNum++;
                manager.changeGear();
            }
        }

        if (engineRPM < minRPM && gearNum > 0)
        {
            gearNum--;
            manager.changeGear();
        }
    }

    private bool isGrounded()
    {
        if (wheels[0].isGrounded && wheels[1].isGrounded && wheels[2].isGrounded && wheels[3].isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void moveVehicle()
    {
        if (IM.boosting)
        {
            totalPower += 2000f;
            //rigidbody.AddForce(transform.forward * thrust);
        }

        if (drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < wheels.Length; ++i)
            {
                wheels[i].motorTorque = totalPower / 4;
            }
        }
        else if (drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; ++i)
            {
                wheels[i].motorTorque = (totalPower / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length - 2; ++i)
            {
                wheels[i].motorTorque = (totalPower / 2);
            }
        }

        KPH = rigidbody.velocity.magnitude * 3.6f;
    }

    private void steerVehicle()
    {
        if (IM.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }
        else if (IM.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    private void animateWheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; ++i)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    private void getObjects()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        IM = GetComponent<InputManager>();
        rigidbody = GetComponent<Rigidbody>();
        colliders = GameObject.Find("Colliders");
        meshes = GameObject.Find("Meshes");
        wheels[0] = colliders.transform.Find("FrontLeftWheel").gameObject.GetComponent<WheelCollider>();
        wheels[1] = colliders.transform.Find("FrontRightWheel").gameObject.GetComponent<WheelCollider>();
        wheels[2] = colliders.transform.Find("RearLeftWheel").gameObject.GetComponent<WheelCollider>();
        wheels[3] = colliders.transform.Find("RearRightWheel").gameObject.GetComponent<WheelCollider>();
        wheelMesh[0] = meshes.transform.Find("FrontLeftWheel").gameObject;
        wheelMesh[1] = meshes.transform.Find("FrontRightWheel").gameObject;
        wheelMesh[2] = meshes.transform.Find("RearLeftWheel").gameObject;
        wheelMesh[3] = meshes.transform.Find("RearRightWheel").gameObject;

        centerOfMass = GameObject.Find("mass");
        rigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void addDownForce()
    {
        rigidbody.AddForce(-transform.up * DownForceValue * rigidbody.velocity.magnitude);
    }

    private void getFriction()
    {
        for (int i = 0; i < wheels.Length; ++i)
        {
            WheelHit wheelHit;
            wheels[i].GetGroundHit(out wheelHit);

            slip[i] = wheelHit.forwardSlip;
        }
    }

    public float driftFactor;

    private void adjustFriction()
    {
        float driftSmothFactor = .7f * Time.deltaTime;

        if (IM.handbrake)
        {
            sidewaysFriction = wheels[0].sidewaysFriction;
            forwardFriction = wheels[0].forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =
                Mathf.SmoothDamp(forwardFriction.asymptoteValue, driftFactor * handBreakFrictionMultiplier, ref velocity, driftSmothFactor);

            for (int i = 0; i < 4; i++)
            {
                wheels[i].sidewaysFriction = sidewaysFriction;
                wheels[i].forwardFriction = forwardFriction;
            }

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue = 1.1f;

            for (int i = 0; i < 2; i++)
            {
                wheels[i].sidewaysFriction = sidewaysFriction;
                wheels[i].forwardFriction = forwardFriction;
            }
            rigidbody.AddForce(transform.forward * (KPH / 400) * 10000);
        }
        else
        {

            forwardFriction = wheels[0].forwardFriction;
            sidewaysFriction = wheels[0].sidewaysFriction;

            forwardFriction.extremumValue = forwardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue =
                ((KPH * handBreakFrictionMultiplier) / 300) + 1;

            for (int i = 0; i < 4; i++)
            {
                wheels[i].forwardFriction = forwardFriction;
                wheels[i].sidewaysFriction = sidewaysFriction;

            }
        }

        for (int i = 2; i < 4; i++)
        {
            WheelHit wheelHit;

            wheels[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip >= 0.3f || wheelHit.sidewaysSlip <= -0.3f || wheelHit.forwardSlip >= .3f || wheelHit.forwardSlip <= -0.3f)
                playPauseSmoke = true;
            else
                playPauseSmoke = false;


            if (wheelHit.sidewaysSlip < 0) driftFactor = (1 + -IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);

            if (wheelHit.sidewaysSlip > 0) driftFactor = (1 + IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
        }
    }

    private IEnumerator timedLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            radius = 6 + KPH / 20;
        }
    }
}
