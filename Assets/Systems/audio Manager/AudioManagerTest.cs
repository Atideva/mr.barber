﻿using UnityEngine;

public class AudioManagerTest : MonoBehaviour
{
    public AudioClip buttonClickSFX;
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip sfx1;
    public AudioClip sfx2;

    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(music1, 0.5f,3f);
        Debug.Log("<color=red>WARNING</color> here is <color=orange>CHEAT</color> in code", gameObject);
    }
    /*
     void Update()
      {

          if (Input.GetKeyDown(KeyCode.Alpha1))
              AudioManager.Instance.PlaySFX(buttonClickSFX, 1);

          if (Input.GetKeyDown(KeyCode.Alpha2))
              AudioManager.Instance.PlayMusic(music1);

          if (Input.GetKeyDown(KeyCode.Alpha3))
              AudioManager.Instance.PlayMusic(music2);

          if (Input.GetKeyDown(KeyCode.Alpha4))
              AudioManager.Instance.PlayMusicWithFade(music1);

          if (Input.GetKeyDown(KeyCode.Alpha5))
              AudioManager.Instance.PlayMusicWithFade(music2);

          if (Input.GetKeyDown(KeyCode.Alpha6))
              AudioManager.Instance.PlayMusicWithCrossFade(music1, 3.0f);

          if (Input.GetKeyDown(KeyCode.Alpha7))
              AudioManager.Instance.PlayMusicWithCrossFade(music2, 3.0f);


      }

      */

}