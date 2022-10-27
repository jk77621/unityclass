using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{
    NavMeshAgent navAgent;
    bool isHit = false;

    [SerializeField] Transform player;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            navAgent.SetDestination(player.position);
        }
        else
        {
            //isHit = false;
            navAgent.isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            isHit = true;
        }
    }
}
