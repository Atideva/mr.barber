using System.Collections;
using UnityEngine;

public class Barber_busted : MonoBehaviour
{
    public iMotor2D_human engine;
 
    void Start()
    {
        Events_Barber.Instance.On_BarberBusted += StopMe;
        EventManager.Instance.On_LevelComplete += StopMe;
    }
    bool stoped;
    void StopMe()
    {
        if (!stoped)
        {
            stoped = true;
            StartCoroutine(Stop());
        }
    }
    IEnumerator Stop()
    {
        while (true)
        {
            engine.StopInstant();
            yield return null;
        }
    }
    
}
