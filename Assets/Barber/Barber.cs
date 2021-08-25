using System;
using System.Collections;
using UnityEngine;

public class Barber : MonoBehaviour
{
    [Header("Move")]
    public float movSpeed;
    public iMotor2D_human engine;
    [Header("Clipper")]
    public bool clipLimited;
    public BoxCollider2D clipperCol;
    public int cliperChargesMax = 1;
    public float cliperCD = 0.7f;
    public float cliperChargesRestoreTime = 0.7f;
    int cliperCharges;
    public bool useFlash;
    public Color clipReady;
    public Color clipUnready;
    public SpriteRenderer clipRend;
    void Start()
    {
        clipRend.color = clipReady;
        engine.SetMoveSpeed(movSpeed);
        Events_Barber.Instance.On_HairClipped += ClipperWorked;
        cliperCharges = cliperChargesMax;
        if (!useFlash) clipRend.enabled = false;
    }

   void ClipperWorked(Vector2 obj,bool isPolice,bool PoliceHair)
    {
        if (!clipLimited) return;

        cliperCharges--;
        StartCoroutine(chargeRestore());
        if (cliperCharges <= 0)
        {
            clipRend.color = clipUnready;
            StartCoroutine(cliperColdown());
        }
    }
    IEnumerator chargeRestore()
    {
        yield return new WaitForSeconds(cliperChargesRestoreTime);
        clipRend.color = clipReady; 
        cliperCharges++;
    }
    IEnumerator cliperColdown()
    {
        clipperCol.enabled = false;
        yield return new WaitForSeconds(cliperCD);
        clipperCol.enabled = true;
    }
    public void MoveDirection(Vector2 dir)
    {
        engine.SetMoveDirection(dir);
        _dir = dir;
    }
    public Vector2 _dir;

}
