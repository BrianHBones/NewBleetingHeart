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
    [SerializeField]
    private AudioSource scanGood;
    [SerializeField]
    private AudioSource scanBad;
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
                if (!scanGood.isPlaying)
                {
                    scanGood.Play();
                }

                coolDoor.SetActive(false);
                doorDisabled = true;
            }
        }
    }

    IEnumerator DoorFail()
    {
        while (true)
        {
            while (hb.fastHeartrate == false && heartBeatDoorRange)
            {
                print("Playing ScanBad");

                if (!scanBad.isPlaying)
                {
                    scanBad.Play();
                    yield return new WaitForSeconds(1);
                }

                yield return null;
            }

            yield return null;
        }
    }
}
