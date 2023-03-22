using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourExtras : MonoBehaviour
{
    private FirstPersonController fpc;

    public AudioClip[] steps;

    // Start is called before the first frame update
    void Start()
    {
        fpc = GetComponent<FirstPersonController>();

        fpc.steps = steps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
