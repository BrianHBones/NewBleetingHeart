using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rateChange = new WaitForSeconds(changeRate);
        StartCoroutine("RateTracker");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            tapTimes.Add(Time.timeSinceLevelLoad);

            /*if(fastHeartrate)
            {
                heartRate += 1;
            }
            else if(regularHeartrate){
                heartRate += 3;
            }
            else if(slowHeartrate){
                heartRate += 5;
            }*/
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

            heartRate = (heartRate + (int)(tapTimes.Count * (60 / timeRange))) / 2;
            yield return rateChange;
        }
    }

    /*private IEnumerator HeartDecrease()
    {
        while (true)
        {
            if (notDead)
            {
                heartRate -= 3;
            }

            yield return new WaitForSeconds(1);
        }
    }*/

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
                print(60f / heartRate);
            }
        }
    }
}
