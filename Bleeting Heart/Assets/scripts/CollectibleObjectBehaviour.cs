/***************************************************************************** 
// File Name :         CollectibleObjectBehaviour.cs 
// Author :            Parker DeVenney 
// Creation Date :     March 1, 2023
// 
// Brief Description : Handles the collectible objects the player needs to 
                       collect in order to flee the level.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObjectBehaviour : MonoBehaviour
{
    public AudioClip ItemObtained;
    [Tooltip("References the Game Controller script.")]
    [SerializeField]
    private GameController gc;

    [Tooltip("References the Player script.")]
    [SerializeField]
    private PlayerBehaviour pb;

    [Tooltip("References the Player and Game Controller game object.")]
    [SerializeField]
    private GameObject player, gameController;

    void Start()
    {
        // Finds the player and gets access to its script
        player = GameObject.FindGameObjectWithTag("Player");
        pb = player.GetComponent<PlayerBehaviour>();

        // Finds the game controller and gets access to its script
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gc = gameController.GetComponent<GameController>();
    }

    /// <summary>
    /// Handles what happens when something collides with the collectible.
    /// </summary>
    /// <param name="collidedObject"> The object that is being colliding with </param>
    void OnCollisionEnter(Collision collidedObject)
    {
        // If the player collides with this object
        if(collidedObject.gameObject.tag == "Player")
        {
            // How many do they have?
            switch (pb.numCollectiblesHeld)
            {
                // If one, tell the player that they have one collectible.
                case 1:
                    gc.DisplayPlayerMessage(pb.numCollectiblesHeld + " item out of " + pb.numCollectiblesNeeded + " collected.");
                    break;
                // If twp, tell the player that they have two collectibles.
                case 2:
                    gc.DisplayPlayerMessage(pb.numCollectiblesHeld + " items out of " + pb.numCollectiblesNeeded + " collected.");
                    break;
                // If any other value, tell the player the number of collectibles they have and that they are able to leave.
                default:
                    gc.DisplayPlayerMessage(pb.numCollectiblesHeld + " items out of " + pb.numCollectiblesNeeded + " collected. \n Now you can escape!");
                    break;
            }
            // Play a sound clip
            AudioSource.PlayClipAtPoint(ItemObtained, gameObject.transform.position);

            // Destroy the game object.
            Destroy(gameObject);
        }
    }
}
