using System;
using UnityEngine;

public class Police : MonoBehaviour
{
    public Police_anim anim;
    public Human_behaviour behaviour;
    public float stayTime = 1f;
    public Human me_human;
    public int wantedLevel;
    public float spdMultINC = 1;
    [Range(0, 0.1f)] public float spdMultINC_perLVL = 1;
    float newSpeed;
    public SpriteRenderer star;
    public float starInc = 0.1f;
    public Human_collider colider;

    public int WantedLevel => wantedLevel;

    void Start()
    {
        Events_Barber.Instance.On_BarberBusted += BustedBastard;
    }

    void BustedBastard()
    {
        behaviour.FollowTarget(null);
    }

    public void CrimeCommited()
    {
        wantedLevel++;
        Events_Police.Instance.CrimeCommited(this, WantedLevel);

        int lvl = Game_level.Instance.level;

        newSpeed = spdMultINC * (lvl * spdMultINC_perLVL + 1) * (WantedLevel + 1);
        if (WantedLevel == 1)
        {
            colider.CrimeSuspected(true);
            me_human.StopMe(true, 0);
            anim.CrimeCommited();
            Invoke(nameof(FollowSuspect), stayTime);
        }
        else
            me_human.StopMe(false, newSpeed);

        anim.WhistleSound();
        float size = 1 + starInc * WantedLevel;
        if (size > 2) size = 2;
        star.transform.localScale = new Vector3(size, size, size);
    }

    void FollowSuspect()
    {
        me_human.StopMe(false, newSpeed);
        behaviour.FollowTarget(My_GameManager.Instance.barber.transform);
    }
}