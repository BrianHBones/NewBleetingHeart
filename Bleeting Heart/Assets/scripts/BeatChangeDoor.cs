using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatChangeDoor : MonoBehaviour
{
    public HeartbeatBehaviour hbb;

    private bool inRange = false;
    private int target;
    public int range;

    // Start is called before the first frame update
    void Start()
    {
        hbb = GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
