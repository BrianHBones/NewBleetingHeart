/***************************************************************************** 
// File Name :         EscapeDoorBehaviour.cs 
// Author :            Parker DeVenney 
// Creation Date :     March 6, 2023
// 
// Brief Description : Handles the logic for the escape door. If 'x' number of
//                     collectible objects are collected, the player escapes.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeDoorBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerBehaviour pb;
    
    [SerializeField]
    private GameController gc;

    [SerializeField]
    private GameObject player, gameController;

    private bool isInRangeOfDoor = false;

    public AudioSource doorSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pb = player.GetComponent<PlayerBehaviour>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gc = gameController.GetComponent<GameController>();
        isInRangeOfDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRangeOfDoor == true)
        {
            if (Input.GetMouseButton(0))
            {
                if (pb.numCollectiblesHeld < 3)
                {
                    Debug.Log("Not enough keys");
                    gc.DisplayPlayerMessage("Not enough keys");
                }
                else if (pb.numCollectiblesHeld >= 3)
                {
                    doorSound.Play();
                    Debug.Log("YOU ESCAPED");
                    Cursor.lockState = CursorLockMode.None;
                    SceneManager.LoadScene("WinScreen");
                    }
            }
        }
    }

    void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.gameObject.tag == "Player")
        {
            Debug.Log("Player in range of door");
            isInRangeOfDoor = true;            
        }
    }
    void OnTriggerExit(Collider collidedObject)
    {
        if (collidedObject.gameObject.tag == "Player")
        {
            Debug.Log("Player left range of door");
            isInRangeOfDoor = false;            
        }
    }
}
