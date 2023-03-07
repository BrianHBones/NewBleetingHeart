using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyBehaviour : MonoBehaviour
{
    public GameObject playerTarget;
    private Vector3 target;

    NavMeshAgent nAgent;

    public Transform[] patrolPoints;
    public int listIndex = 0;
    public int detectRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
        detectRadius = 15;
    }

    // Update is called once per frame
    void Update()
    {
        target = patrolPoints[listIndex].position;

        if (playerTarget == null)
        {
            nAgent.destination = target;
        }

        // Checks if enemy hits the patrol point and sets target to the next point
        if(gameObject.transform.position.x == target.x && gameObject.transform.position.z == target.z)
        {
            if(listIndex == patrolPoints.Length - 1)
            {
                listIndex = 0;
            }
            else
            {
                listIndex++;
            }
        }

        if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().fastHeartrate == true)
        {
            detectRadius = 25;
        }
        else if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().regularHeartrate == true)
        {
            detectRadius = 15;
        }
        else if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().slowHeartrate == true)
        {
            detectRadius = 5;
        }
        else
        {
            detectRadius = 0;
        }
    }
}
