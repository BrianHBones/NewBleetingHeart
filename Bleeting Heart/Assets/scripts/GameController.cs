/***************************************************************************** 
// File Name :         GameController.cs 
// Author :            Parker DeVenney 
// Creation Date :     March 6, 2023
// 
// Brief Description : Handles all of the functions required to operate the 
//                     game on a systemic level.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text playerMessage;

    // Start is called before the first frame update
    void Start()
    {
        playerMessage.text = (" ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayPlayerMessage(string input)
    {
        playerMessage.text = input;
        StartCoroutine(ClearMessage());
    }

    IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(3f);
        playerMessage.text = (" ");
    }
}
