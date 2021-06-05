using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    const int OVERWORLD = 0;
    const int TRANSITION = 1;
    const int CAVE = 2;

    public double musicVolume = 0; //from 0 - 10
    public int songNum = OVERWORLD;

    public AudioSource overworldTheme;
    public AudioSource transitionTheme;
    public AudioSource caveTheme;
    //public AudioSource pauseMusic;

    public Slider volSlider;
    public bool isMusicPaused;



    void Start()
    {
        overworldTheme.volume = (float)musicVolume / 10;
        transitionTheme.volume = (float)musicVolume / 10;
        caveTheme.volume = (float)musicVolume / 10;
        //pauseMusic.volume = (float)musicVolume / 10;
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
      if (!isMusicPaused)
      {
        isMusicPaused = true;
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

    }
    public void unpauseSong()
    {
      if (isMusicPaused)
      {
        isMusicPaused = false;
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
    public void changeVolume()
    {
      musicVolume = volSlider.value;
      //Debug.Log("changing value" + musicVolume.ToString());

      float normalizedVolume = (float)musicVolume / 10;

      overworldTheme.volume = normalizedVolume;
      transitionTheme.volume = normalizedVolume;
      caveTheme.volume = normalizedVolume;
      //pauseMusic.volume = normalizedVolume;
    }

    public void toggleMusic()
    {
      if (isMusicPaused)
      {
        //pauseMusic.Play();
        unpauseSong();
      }
      else
      {
        //pauseMusic.Pause();
        pauseSong();
      }
    }

}
