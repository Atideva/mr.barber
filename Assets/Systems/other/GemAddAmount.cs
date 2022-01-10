using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAddAmount : MonoBehaviour
{
    public int gemValue;
    public AudioEvent sound;
    private const string ThisKey = "Barber_gems_total";
    private bool onceTime;
    public AudioSource source;
    public void AddGem()
    {
        if (onceTime) return;
        onceTime = true;
        
        source.Play();
        // AudioManager.Instance.PlaySimpleEvent(sound);
        int gems = PlayerPrefs.GetInt(ThisKey);
        gems += gemValue;
        PlayerPrefs.SetInt(ThisKey, gems);
    }
}