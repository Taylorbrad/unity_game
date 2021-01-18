using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlaying : MonoBehaviour
{
    const double MUSIC_VOLUME = 0.0; //from 0.0 - 1.0

    public AudioSource overworldTheme;
    public AudioSource transitionTheme;
    public AudioSource caveTheme;
    public int songNum = 0;



    void Start()
    {
        overworldTheme.volume = (float)MUSIC_VOLUME;
        transitionTheme.volume = (float)MUSIC_VOLUME;
        caveTheme.volume = (float)MUSIC_VOLUME;
        overworldTheme.Play();


    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position[0]);
        switch (songNum)
        {
            case 0:
                if (transform.position[0] > 30)
                {
                    overworldTheme.Stop();
                    transitionTheme.Play();
                    songNum = 1;
                }
                break;
            case 1:
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
            case 2:
                if (transform.position[0] < 150)
                {
                    transitionTheme.Stop();
                    caveTheme.Play();
                    songNum = 1;
                }
                break;
        }
    }
}
