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
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public Text playerMessage;
    public TextMeshProUGUI heartText;

    public GameObject pauseMenu, controlsMenu;
    public GameObject redLight, yellowLight, greenLight;
    HeartbeatBehaviour heartbeat;

    [SerializeField]
    public bool isPaused;

    private void Awake() {
        heartbeat = FindObjectOfType<HeartbeatBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMessage.text = (" ");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        heartText.text = heartbeat.returnHeartrate().ToString("");
        if(heartbeat.fastHeartrate == true){
            greenLight.SetActive(true);
            yellowLight.SetActive(false);
            redLight.SetActive(false);
        }
        if(heartbeat.regularHeartrate == true){
            greenLight.SetActive(false);
            yellowLight.SetActive(true);
            redLight.SetActive(false);
        }
        if(heartbeat.slowHeartrate == true){
            greenLight.SetActive(false);
            yellowLight.SetActive(false);
            redLight.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);            
            Cursor.lockState = CursorLockMode.None;
        }
        else if (isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            controlsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenControlsMenu()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    
    public void CloseControlsMenu()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
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
