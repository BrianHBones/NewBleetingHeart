using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartbeatDoor : MonoBehaviour
{
    public HeartbeatBehaviour hb;

    [SerializeField]
    private GameObject coolDoor;

    private bool heartBeatDoorRange = false;
    private bool doorDisabled = false;
    public AudioClip doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        hb = hb.GetComponent<HeartbeatBehaviour>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {  
            heartBeatDoorRange = true;            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
                AudioSource.PlayClipAtPoint(doorOpen, gameObject.transform.position);
                coolDoor.SetActive(false);
                doorDisabled = true;
            }
        }
    }
}
