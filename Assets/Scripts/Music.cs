using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    const int OVERWORLD = 0;
    const int TRANSITION = 1;
    const int CAVE = 2;

    public double musicVolume = 0; //from 0.0 - 1.0
    public int songNum = OVERWORLD;

    public AudioSource overworldTheme;
    public AudioSource transitionTheme;
    public AudioSource caveTheme;





    void Start()
    {
        overworldTheme.volume = (float)musicVolume / 10;
        transitionTheme.volume = (float)musicVolume / 10;
        caveTheme.volume = (float)musicVolume / 10;
        overworldTheme.Play();


    }

    void Update()
    {
        //Debug.Log(transform.position[0]);
        switch (songNum)
        {
            case OVERWORLD:
                if (transform.position[0] > 30)
                {
                    overworldTheme.Stop();
                    transitionTheme.Play();
                    songNum = 1;
                }
                break;
            case TRANSITION:
                if (transform.position[0] > 150)
                {
                    transitionTheme.Stop();
                    caveTheme.Play();
                    songNum = 2;
                }
                else if (transform.position[0] < 30)
                {
                    transitionTheme.Stop();
                    overworldTheme.Play();
                    songNum = 0;
                }
                break;
            case CAVE:
                if (transform.position[0] < 150)
                {
                    transitionTheme.Stop();
                    caveTheme.Play();
                    songNum = 1;
                }
                break;
        }
    }
    public void pauseSong()
    {
      switch (songNum)
      {
        case OVERWORLD:
          overworldTheme.Pause();
          break;
        case TRANSITION:
          transitionTheme.Pause();
          break;
        case CAVE:
          caveTheme.Pause();
          break;
      }
    }
    public void unpauseSong()
    {
      switch (songNum)
      {
        case OVERWORLD:
          overworldTheme.Play();
          break;
        case TRANSITION:
          transitionTheme.Play();
          break;
        case CAVE:
          caveTheme.Play();
          break;
      }
    }
}
