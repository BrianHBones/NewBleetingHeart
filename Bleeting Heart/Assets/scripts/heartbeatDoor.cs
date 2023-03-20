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
    private AudioSource scanGood;
    private AudioSource scanBad;
    // Start is called before the first frame update
    void Start()
    {
        hb = hb.GetComponent<HeartbeatBehaviour>();
        scanGood = transform.GetChild(1).GetComponent<AudioSource>();
        scanBad = transform.GetChild(2).GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            heartBeatDoorRange = true;
            StartCoroutine("DoorFail");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            heartBeatDoorRange = false;
            StopCoroutine("DoorFail");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (heartBeatDoorRange == true && doorDisabled == false)
        {
            if (hb.fastHeartrate == true)
            {
                coolDoor.SetActive(false);
                doorDisabled = true;
                scanGood.Play();
            }
        }
    }

    IEnumerator DoorFail()
    {
        while (hb.fastHeartBeat == false && heartBeatDoorRange)
        {
            scanBad.Play();
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }
}
