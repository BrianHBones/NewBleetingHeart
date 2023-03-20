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
    void OnCollisionEnter(Collision collidedObject)
    {
        if(collidedObject.gameObject.tag == "Player")
        {
            Debug.Log("Item collected.");
            AudioSource.PlayClipAtPoint(ItemObtained, gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
