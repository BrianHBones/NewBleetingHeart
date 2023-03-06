using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatBehaviour : MonoBehaviour
{
  public  bool Heartbeat;
  public float startTime;
  public float Health;
  public float heartTime;
  public float ouchScalar;
  public bool regularHeartrate;
  public bool slowHeartrate;
  public bool fastHeartrate;
    // Start is called before the first frame update
    void Start()
    {
        Heartbeat = false;
        Health = 10;
        heartTime = 0;
        ouchScalar = 1;
    }

    // Update is called once per frame
    void Update()
    {
        heartTime += Time.deltaTime;
       
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Heartbeat = true;
               
          if (heartTime < 0.25 || heartTime > 5)
          {
                    Health -= ouchScalar;
          }
          else if (heartTime < 3 && heartTime > 2)
          {
                regularHeartrate = true;
                fastHeartrate = false;
                slowHeartrate = false;
          }
          else if (heartTime < 5 && heartTime > 3)
          {
                regularHeartrate = false;
                fastHeartrate = false;
                slowHeartrate = true;
          }
          else if (heartTime < 2 && heartTime > 0.25)
          {
                regularHeartrate = false;
                fastHeartrate = true;
                slowHeartrate = false;
          }
           
                
                
                heartTime = 0;
        }
        else
        {
            Heartbeat = false;
        }

     

 
    }
}
