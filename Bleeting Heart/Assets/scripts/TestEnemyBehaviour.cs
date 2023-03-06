using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyBehaviour : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent nAgent;

    // Start is called before the first frame update
    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nAgent.destination = target.transform.position;
    }
}
