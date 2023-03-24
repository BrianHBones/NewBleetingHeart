using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartbeatBehaviour : MonoBehaviour
{
    public float Health = 3;
    public int heartRate = 60;

    public List<float> tapTimes;
    public float timeRange;
    public float freeTime;
    public float changeRate;
    public int[] rateGates;
    private WaitForSeconds rateChange;

    public bool regularHeartrate;
    public bool slowHeartrate;
    public bool fastHeartrate;
    public bool notDead = true;
    public AudioSource heartBeatSound;
    /*public AudioClip normalHeartBeat;
    public AudioClip fastHeartBeat;
    public AudioClip slowHeartBeat;*/
    public float timer;
    public GameObject player;
    public bool hardMode;

    // Start is called before the first frame update
    void Start()
    {
        rateChange = new WaitForSeconds(changeRate);

        if (hardMode)
        {
            StartCoroutine("RateTracker");
        }
        else
        {
            StartCoroutine("HeartDecrease");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (hardMode)
            {
                tapTimes.Add(Time.timeSinceLevelLoad);
            }
            else
            {
                if (fastHeartrate)
                {
                    heartRate += 1;
                }
                else if (regularHeartrate)
                {
                    heartRate += 3;
                }
                else if (slowHeartrate)
                {
                    heartRate += 5;
                }
            }
        }

        if (Time.timeSinceLevelLoad > freeTime)
        {
            if (heartRate <= rateGates[0])
            {
                slowHeartrate = true;
                regularHeartrate = false;
                fastHeartrate = false;
                //heartBeatSound.clip = slowHeartBeat;
            }
            else if (heartRate <= rateGates[1] && heartRate > rateGates[0])
            {
                slowHeartrate = false;
                regularHeartrate = true;
                fastHeartrate = false;
                //heartBeatSound.clip = normalHeartBeat;
            }
            else if (heartRate <= rateGates[2] && heartRate > rateGates[1])
            {
                slowHeartrate = false;
                regularHeartrate = false;
                fastHeartrate = true;
                //heartBeatSound.clip = fastHeartBeat;
            }
            else if (heartRate > rateGates[2] || heartRate <= 0)
            {
                SceneManager.LoadScene("LoseScreen");
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (!hardMode)
        {
            if (heartRate <= 40)
            {
                slowHeartrate = true;
                regularHeartrate = false;
                fastHeartrate = false;
                //heartBeatSound.clip = slowHeartBeat;
            }
            else if (heartRate <= 75 && heartRate > 40)
            {
                slowHeartrate = false;
                regularHeartrate = true;
                fastHeartrate = false;
                //heartBeatSound.clip = normalHeartBeat;
            }
            else if (heartRate <= 100 && heartRate > 75)
            {
                slowHeartrate = false;
                regularHeartrate = false;
                fastHeartrate = true;
                //heartBeatSound.clip = fastHeartBeat;
            }
            else if (heartRate > 100 || heartRate <= 0)
            {
                SceneManager.LoadScene("LoseScreen");
                Cursor.lockState = CursorLockMode.None;
            }
        }

        HeartBeat();
    }

    private IEnumerator RateTracker()
    {
        while (true)
        {
            for (int x = 0; x < tapTimes.Count; x++)
            {
                if (Time.timeSinceLevelLoad - tapTimes[x] > timeRange)
                {
                    tapTimes.RemoveAt(x);
                    x--;
                }
            }

            heartRate = (int)(tapTimes.Count * (60 / timeRange));
            yield return rateChange;
        }
    }

    private IEnumerator HeartDecrease()
    {
        while (true)
        {
            if (notDead)
            {
                heartRate -= 3;
            }

            yield return new WaitForSeconds(1);
        }
    }

    public int returnHeartrate()
    {
        return heartRate;
    }

    private void HeartBeat()
    {
        timer += Time.deltaTime;

        if (timer >= 60f / heartRate)
        {
            if (!heartBeatSound.isPlaying)
            {
                timer = 0;

                heartBeatSound.Play();
                //print(60f / heartRate);
            }
        }
    }
}
