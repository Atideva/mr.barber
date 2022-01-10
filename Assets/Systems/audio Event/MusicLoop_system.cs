using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop_system : MonoBehaviour
{
    public float changeTime = 1f;

    [Header("Main")]
    public int menu = 4;
    [Range(0, 1)] public float menu_VolMax = 1;

    [Header("Play")]
    public int playGame = 5;
    [Range(0, 1)] public float playGame_VolMax = 1;

    [Header("Play")]
    public int police1 = 5;
    [Range(0, 1)] public float police1_VolMax = 1;
    public int police2 = 5;
    [Range(0, 1)] public float police2_VolMax = 1;

    [Header("Volumes")]
    [Range(0, 1)] public List<float> volume = new List<float>();
    List<AudioSource> sourse = new List<AudioSource>();
    void Start()
    {
        var sourses = GetComponentsInChildren<AudioSource>();
        foreach (var item in sourses)
        {
            sourse.Add(item);
            volume.Add(0);
        }

        menu--;
        playGame--;
        police1--;
        police2--;

        volume[menu] = menu_VolMax;
        EventManager.Instance.On_PlayButton_Pressed += LevelStarted;
        Events_Barber.Instance.On_BarberBusted += LevelFail;
        Events_Police.Instance.On_At_Duty += PolicePursuit;
        Events_Police.Instance.On_No_On_Duties += PoliceLostTarget;
        EventManager.Instance.On_LevelComplete += PoliceLostTarget;

    }

    void PolicePursuit()
    {
        StartCoroutine(ChangeVol(police1, police1_VolMax, changeTime));
        StartCoroutine(ChangeVol(police2, police2_VolMax, changeTime));
        StartCoroutine(ChangeVol(playGame, 0, changeTime));

    }
    void PoliceLostTarget()
    {
        StartCoroutine(ChangeVol(police1, 0, changeTime));
        StartCoroutine(ChangeVol(police2, 0, changeTime));
        StartCoroutine(ChangeVol(playGame, playGame_VolMax, changeTime));
    }


    void LevelStarted() => StartCoroutine(ChangeVol(playGame, playGame_VolMax, changeTime));
    void LevelFail()
    {
        StartCoroutine(ChangeVol(playGame, 0, changeTime));
        StartCoroutine(ChangeVol(police1, 0, changeTime));
        StartCoroutine(ChangeVol(police2, 0, changeTime));
    }

    void FixedUpdate()
    {
        for (int i = 0; i < sourse.Count; i++)
        {
            sourse[i].volume = volume[i];
        }
    }

    IEnumerator ChangeVol(int id, float to, float time)
    {
        float vol = volume[id];
        float dif = (to - vol);
        float n = (dif >= 0) ? 1 : -1;

        float step = Mathf.Abs(dif) * Time.deltaTime / time;
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;

            vol += n * step;
            vol = Mathf.Clamp01(vol);
            volume[id] = vol;
            yield return null;
        }
        volume[id] = to;
    }
}
