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

    // Start is called before the first frame update
    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        target = patrolPoints[listIndex].position;

        if (playerTarget == null)
        {
            nAgent.destination = target;
        }

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
    }
}
