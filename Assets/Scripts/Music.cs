using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource DriveM;
    [SerializeField] AudioSource StartSound;
    [SerializeField] AudioSource KenSound;
    [SerializeField] AudioSource KenMusic;
    [SerializeField] AudioSource LaLaSound;
    [SerializeField] AudioSource LaLaMusic;

    public static bool TimeToPlay;
    private bool TimeToSLeep = false;

    void Start()
    {
        DriveM.enabled = true;
    }

    void Update()
    {
        if (GameManager.isItGameOver)
        {
            DriveM.enabled = false;
            StartSound.enabled = false;
            KenSound.enabled = false;
            KenMusic.enabled = false;   
            LaLaSound.enabled = false;
            LaLaMusic.enabled = false;
        }

        var timeSinceStart = Time.time;

        if (timeSinceStart > 22)
            TimeToSLeep = true;

        if (TimeToSLeep && DriveM.volume > 0)
            DriveM.volume -= 0.0005f;

        if (TimeToPlay)
        {
            StartSound.enabled = true;
            StartSound.Play();
            TimeToPlay = false;

            if (Ryan.skin == "Ken")
            {
                KenMusic.enabled = true;
                KenMusic.Play();
            }

            if (Ryan.skin == "LaLaLand")
            {
                LaLaMusic.enabled = true;
                LaLaMusic.Play();
            }
        }


        if (Ryan.skin == "Ken" && Arrows.SkinIsChange)
        {
            KenSound.enabled = true;
            KenSound.Play();
            Arrows.SkinIsChange = false;
        }

        if (Ryan.skin == "LaLaLand" && Arrows.SkinIsChange)
        {
            LaLaSound.enabled = true;
            LaLaSound.Play();
            Arrows.SkinIsChange = false;
        }
    }
}
