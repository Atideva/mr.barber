using System;
using UnityEngine;

public class Human_drop_coins : MonoBehaviour
{
    public int countMax;
    public float delay;
    public AudioEvent dropSound;
    bool dropped;
    void Start()
    {
        //Events_Main.Instance.On_LevelComplete += StopDrop;
    }
    bool stopDrop;
    void StopDrop()
    {
        stopDrop = true;
    }

    public void DropCoins()
    {
        if (!dropped)
        {
            dropped = true;
            Invoke("Drop", delay);
        }
    }

    void Drop()
    {
        if (stopDrop) return;
        if (dropSound) AudioManager.Instance.PlaySimpleEvent(dropSound);
        int r = UnityEngine.Random.Range(1, countMax);
        Events_Resourses.Instance.CoinDrop(transform.position, r);
    }


}
