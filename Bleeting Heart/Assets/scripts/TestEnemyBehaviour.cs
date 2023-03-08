using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyBehaviour : MonoBehaviour
{
    public GameObject playerTarget;
    public Vector3 target;

    NavMeshAgent nAgent;

    public Transform[] patrolPoints;
    public int listIndex = 0;
    public int detectRadius;

    public bool chase;
    public float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
        detectRadius = 15;
        chase = false;
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        /// target is set to the next patrol point's position.
        target = patrolPoints[listIndex].position;

        /// If it isn't chasing the player, patrol.
        if (playerTarget == null)
        {
            nAgent.destination = target;
        }
        else
        {
            nAgent.destination = playerTarget.transform.position;
        }

        /// If the player is close enough to the enemy, set timer to 5 and begin chasing player.
        /// If the player is far enough away from enemy, begin decreasing timer.
        /// If timer runs out, stop chasing and resume patrolling.
        if(Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < detectRadius)
        {
            timer = 5;
            chase = true;
        }
        else if(Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) > detectRadius)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                chase = false;
            }
        }

        /// Checks if enemy hits the patrol point and sets target to the next point.
        if(chase == false)
        {
            playerTarget = null;
            nAgent.speed = 2;
            if (gameObject.transform.position.x == target.x && gameObject.transform.position.z == target.z)
            {
                if (listIndex == patrolPoints.Length - 1)
                {
                    listIndex = 0;
                }
                else
                {
                    listIndex++;
                }
            }
        }
        else
        {
            nAgent.speed = 4;
            playerTarget = GameObject.Find("Player");
        }

        /// Changes the enemy detection radius based on the player's heartbeat.
        if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().fastHeartrate == true)
        {
            detectRadius = 16;
        }
        else if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().regularHeartrate == true)
        {
            detectRadius = 10;
        }
        else if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().slowHeartrate == true)
        {
            detectRadius = 5;
        }
        else
        {
            detectRadius = 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
