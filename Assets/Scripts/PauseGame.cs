using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    bool isPaused = false;
    //public Music Music;
    public Music musicScript;

    public void pauseUnpauseGame()
    {
        if (isPaused)
        {
          Time.timeScale = 1;
          isPaused = false;
          musicScript.unpauseSong();
          //.getComponent<Music>().pauseSong();
        }
        else
        {
          Time.timeScale = 0;
          isPaused = true;
          musicScript.pauseSong();
        }
    }
}
