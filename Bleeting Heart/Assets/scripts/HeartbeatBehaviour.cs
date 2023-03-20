using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatBehaviour : MonoBehaviour
{
    public float Health = 3;
    public int heartRate = 60;
    public bool regularHeartrate;
    public bool slowHeartrate;
    public bool fastHeartrate;
    public bool notDead = true;
    public AudioSource normalHeartBeat;
    public AudioSource fastHeartBeat;
    public AudioSource slowHeartBeat;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("HeartDecrease");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(fastHeartrate){
                heartRate += 1;
            }
            else if(regularHeartrate){
                heartRate += 3;
            }
            else if(slowHeartrate){
                heartRate += 5;
            }
        }
        if(heartRate <= 40){
            slowHeartrate = true;
            regularHeartrate = false;
            fastHeartrate = false;
            
        }
        if(heartRate <= 75 && heartRate > 40){
            slowHeartrate = false;
            regularHeartrate = true;
            fastHeartrate = false;
           
        }
        if(heartRate <= 100 && heartRate > 75){
            slowHeartrate = false;
            regularHeartrate = false;
            fastHeartrate = true;
            
        }
        if (regularHeartrate == true)
        {
            print("heart");
            normalHeartBeat.Play();
            fastHeartBeat.Pause();
            slowHeartBeat.Pause();
        }
        if (slowHeartrate == true)
        {
            normalHeartBeat.Pause();
            fastHeartBeat.Pause();
            slowHeartBeat.Play();
        }
        if (fastHeartrate == true)
        {
            normalHeartBeat.Pause();
            fastHeartBeat.Play();
            slowHeartBeat.Pause();
        }

    }

    private IEnumerator HeartDecrease(){
        while(true){
            if(notDead){
                heartRate -= 3;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public int returnHeartrate(){
        return heartRate;
    }
}
