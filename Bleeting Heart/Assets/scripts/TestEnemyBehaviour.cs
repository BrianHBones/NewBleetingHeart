using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject enemyChase;

    public AudioSource growl;
    public AudioSource[] step = new AudioSource[3];
    public AudioClip[] steps;
    public float[] timer_ = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
        detectRadius = 15;
        chase = false;
        timer = 5f;
        enemyChase.SetActive(false);

        StartCoroutine("StepSounds1");
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
            enemyChase.SetActive(false);
            playerTarget = null;
            nAgent.speed = 3;
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
            
            if (growl.isPlaying)
            {
                growl.loop = false;
            }
        }
        else
        {
            nAgent.speed = 4.5f;
            playerTarget = GameObject.Find("Player");

            if (!growl.isPlaying)
            {
                growl.Play();
                growl.loop = true;
            }

            enemyChase.SetActive(true);
        }

        /// Changes the enemy detection radius based on the player's heartbeat.
        if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().fastHeartrate == true)
        {
            detectRadius = 19;
        }
        else if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().regularHeartrate == true)
        {
            detectRadius = 12;
        }
        else if(GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>().slowHeartrate == true)
        {
            detectRadius = 5;
        }
        else
        {
            detectRadius = 1;
        }

        //StepSounds();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Time.timeScale = 0;
            SceneManager.LoadScene("LoseScreen");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator StepSounds1()
    {
        yield return new WaitForSeconds((1f / 3f) * (1f / 3.4f));
        StartCoroutine("StepSounds2");

        while (true)
        {
            step[0].Stop();
            int step_ = Random.Range(0, steps.Length);
            step[0].clip = steps[step_];

            step[0].Play();

            yield return new WaitForSeconds(1f / 3.4f);
        }
    }

    IEnumerator StepSounds2()
    {
        yield return new WaitForSeconds((1f / 3f) * (1f / 3.4f));
        StartCoroutine("StepSounds3");

        while (true)
        {
            step[1].Stop();
            int step_ = Random.Range(0, steps.Length);
            step[1].clip = steps[step_];

            step[1].Play();

            yield return new WaitForSeconds(1f / 3.4f);
        }
    }

    IEnumerator StepSounds3()
    {
        yield return new WaitForSeconds((1f / 3f) * (1f / 3.4f));

        while (true)
        {
            step[2].Stop();
            int step_ = Random.Range(0, steps.Length);
            step[2].clip = steps[step_];

            step[2].Play();

            yield return new WaitForSeconds(1f / 3.4f);
        }
    }

    void StepSounds()
    {
        for (int x = 0; x < timer_.Length; x++)
        {
            timer_[x] += Time.deltaTime;

            if (timer_[x] >= 1.0f / 3.4f)
            {
                if (!step[x].isPlaying)
                {
                    timer_[x] = 0;

                    int step_ = Random.Range(0, steps.Length);
                    step[x].clip = steps[step_];

                    step[x].Play();
                }
            }
        }
    }
}
