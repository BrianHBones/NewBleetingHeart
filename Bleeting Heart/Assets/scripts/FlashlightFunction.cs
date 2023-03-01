using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFunction : MonoBehaviour
{

    public bool lightOn = false;
    public GameObject lightSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lightOn == false)
            {
                lightSource.SetActive(true);
                lightOn = true;
            }

            else if (lightOn == true)
            {
                lightSource.SetActive(false);
                lightOn = false;
            }
        }
    }
}
