/***************************************************************************** 
// File Name :         PlayerBehaviour.cs 
// Author :            Parker DeVenney 
// Creation Date :     March 1, 2023
// 
// Brief Description : Handles the variables connected to the player in order
                       to handle win and lose conditions.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Tooltip("Value of the number of items the player needs to escape.")]
    public int numCollectiblesNeeded;

    [Tooltip("Value of the number of items the player currently holds.")]
    public int numCollectiblesHeld;

    public bool isHidden = false;

    public GameObject isHiddenText;

    // Start is called before the first frame update
    void Start()
    {
        numCollectiblesNeeded = 3;
        numCollectiblesHeld = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Clicked");
        }
        if (numCollectiblesHeld >= numCollectiblesNeeded)
        {
            
        }
    }

    void IncreaseNumCollectibles(int amt)
    {
        numCollectiblesHeld += amt;
    }
    void DecreaseNumCollectibles(int amt)
    {
        if (numCollectiblesHeld > 0)
        {
            numCollectiblesHeld -= amt;
        }        
    }

    void OnCollisionEnter(Collision collidedObject)
    {
        if (collidedObject.gameObject.tag == "Collectible")
        {
            IncreaseNumCollectibles(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HideZone")
        {
            isHidden = true;
            isHiddenText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HideZone")
        {
            isHidden = false;
            isHiddenText.SetActive(false);
        }
    }
}
