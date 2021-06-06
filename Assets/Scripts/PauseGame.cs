using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public static bool isPaused = false;
    public static bool isOptionsOpen = false;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;

    //public Music Music;
    public Music musicComponentFromPlayer;
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        pauseUnpauseGame();
      }
    }
    public void pauseUnpauseGame()
    {
      //Debug.Log("pause function");
        if (isPaused) // unpause
        {
          pauseMenuUI.SetActive(false);
          Time.timeScale = 1;
          isPaused = false;
          isOptionsOpen = false;
          optionsMenuUI.SetActive(false);
          mainMenuUI.SetActive(true);
          musicComponentFromPlayer.unpauseSong();
          //.getComponent<Music>().pauseSong();
        }
        else //pause
        {
          pauseMenuUI.SetActive(true);
          Time.timeScale = 0;
          isPaused = true;
          musicComponentFromPlayer.pauseSong();
        }
    }
    public void optionsMenuOpenClose()
    {
      if (isOptionsOpen) //close
      {
        isOptionsOpen = false;
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
      }
      else
      {
        isOptionsOpen = true;
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
      }
    }
}
