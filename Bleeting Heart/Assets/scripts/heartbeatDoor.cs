using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartbeatDoor : MonoBehaviour
{
    private HeartbeatBehaviour hb;

    [SerializeField]
    private GameObject coolDoor;

    private bool heartBeatDoorRange = false;
    private bool doorDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        hb = hb.GetComponent<HeartbeatBehaviour>();
    }

    void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.gameObject.tag == "Player")
        {  
            heartBeatDoorRange = true;            
        }
    }

    void OnTriggerExit(Collider collidedObject)
    {
        if (collidedObject.gameObject.tag == "Player")
        {
            heartBeatDoorRange = false;            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (heartBeatDoorRange == true && doorDisabled == false)
        {
            if(hb.fastHeartrate == true)
            {
                coolDoor.SetActive(false);
                doorDisabled = true;
            }
        }
    }
}
